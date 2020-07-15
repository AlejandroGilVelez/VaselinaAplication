using Framework.Dtos;
using Framework.Models;
using Framework.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaselinaWeb.API.Utilidades;
using VaselinaWeb.DataModel.Repositories;

namespace VaselinaWeb.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Atributos

        private readonly IUserRepository userRepository;
        private readonly ICambioPasswordRepository cambioPasswordRepository;
        private readonly IConfiguration configuration;

        #endregion

        #region Constructor
        
        public UserController(IUserRepository userRepository, ICambioPasswordRepository cambioPasswordRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.cambioPasswordRepository = cambioPasswordRepository;
            this.configuration = configuration;
        }

        #endregion

        #region Acciones

        /// <summary>
        /// Metodo que retorna la lista de usuarios.
        /// </summary>
        /// <returns></returns>

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var result = await userRepository.GetAll();

            if (result == null)
            {
                return NotFound();
            }

            List<UserDto> userList = new List<UserDto>();

            foreach (var item in result)
            {
                userList.Add(new UserDto
                {
                    Id = item.Id,
                    NroIdentificacion = item.NroIdentificacion,
                    Nombres = item.Nombres,
                    Apellidos = item.Apellidos,
                    Correo = item.Correo,
                    Telefono = item.Telefono,
                    Activo = item.Activo,
                    CambioPassword = item.CambioPassword
                });
            }

            return Ok(userList);
        }

        /// <summary>
        /// Metodo que retorna un usuario.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await userRepository.Find(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            var usuario = new UserDto
            {
                Id = result.Id,
                NroIdentificacion = result.NroIdentificacion,
                Nombres = result.Nombres,
                Apellidos = result.Apellidos,
                Correo = result.Correo,
                Telefono = result.Telefono,
                Activo = result.Activo,
                CambioPassword = result.CambioPassword
            };


            return Ok(usuario);
        }

        /// <summary>
        /// Metodo que elimina un usuario.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await userRepository.Find(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            await userRepository.Delete(result);

            return Ok();
        }

        /// <summary>
        /// Metodo que crea un usuario.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UserDto user)
        {
            var result = await userRepository.Find(x => x.Correo == user.Correo);

            if (result != null)
            {
                return BadRequest("El correo ya existe");
            }

            user.Password = user.NroIdentificacion.ToString();

            byte[] passwordHash, passwordSalt;

            UtilsPassword.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            var usuario = new User
            {
                Id = Guid.NewGuid(),
                NroIdentificacion = user.NroIdentificacion,
                Nombres = user.Nombres,
                Apellidos = user.Apellidos,
                Correo = user.Correo,
                Telefono = user.Telefono,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Activo = user.Activo,
                CambioPassword = false,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            usuario = await userRepository.Add(usuario);

            var objCambioPassword = await cambioPasswordRepository.Add(new CambioPassword
            {
                Id = Guid.NewGuid(),
                MinutosExpiracion = 120,
                Usuario = usuario,
                Activo = true,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            });

            var sender = configuration.GetSection("Settings").GetSection("EnvioCorreo").GetSection("Sender").Value;
            var senderPassword = configuration.GetSection("Settings").GetSection("EnvioCorreo").GetSection("Password").Value;

            UtilsSendEmail.SendEmailChangePassword(usuario.Correo, $"({usuario.Nombres} {usuario.Apellidos})", objCambioPassword.Id.ToString(), sender, senderPassword);

            return Ok();
        }

        /// <summary>
        /// Metodo que edita un usuario.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UserDto user)
        {
            var usuario = await userRepository.Find(x => x.Id == user.Id);

            if (usuario == null)
            {
                return BadRequest("El usuario no existe");
            }

            usuario.NroIdentificacion = user.NroIdentificacion;
            usuario.Nombres = user.Nombres;
            usuario.Apellidos = user.Apellidos;
            usuario.Correo = user.Correo;
            usuario.Telefono = user.Telefono;
            usuario.FechaCreacion = DateTime.Now;
            usuario.Activo = user.Activo;

            await userRepository.Edit(usuario);

            return Ok();

        }

        [HttpPost("CambioEstado")]
        public async Task<IActionResult> CambioEstado([FromBody] CambioEstadoDto user)
        {
            var usuario = await userRepository.Find(x => x.Id == user.Id);

            if (usuario == null)
            {
                return BadRequest("El usuario no existe");
            }

            usuario.Activo = user.Activo;

            await userRepository.Edit(usuario);

            return Ok();
        }

        #endregion
    }
}