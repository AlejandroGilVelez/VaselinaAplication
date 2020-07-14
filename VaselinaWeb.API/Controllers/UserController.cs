using Framework.Dtos;
using Framework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        #endregion

        #region Constructor
        
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
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
                    Activo = item.Activo                    
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
                Activo = result.Activo
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
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            await userRepository.Add(usuario);

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