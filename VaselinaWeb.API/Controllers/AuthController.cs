using Framework.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VaselinaWeb.DataModel.Repositories;

namespace VaselinaWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Atributos

        private readonly IUserRepository userRepository;

        #endregion


        #region Constructor

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        #endregion


        #region Acciones

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginInformtation)
        {
            var userSeleccionado = await userRepository.ObtenerPorCorreo(loginInformtation.Correo);

            if (userSeleccionado == null)
            {
                return Unauthorized();
            }

            if (!Utilidades.UtilsPassword.VerifyPasswordHash(loginInformtation.Password,userSeleccionado.PasswordHash, userSeleccionado.PasswordSalt))
            {
                return Unauthorized();
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userSeleccionado.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{userSeleccionado.Nombres} {userSeleccionado.Apellidos}"),
                new Claim(ClaimTypes.Role, userSeleccionado.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Miclavedecontraseña"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });

        }

        #endregion

    }
}