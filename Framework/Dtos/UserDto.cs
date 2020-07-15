using System;

namespace Framework.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public long NroIdentificacion { get; set; }
        
        public string Nombres { get; set; }
        
        public string Apellidos { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public string Password { get; set; }

        public bool Activo { get; set; }

        public bool CambioPassword { get; set; }
    }
}
