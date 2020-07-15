using System.ComponentModel.DataAnnotations;

namespace Framework.Models
{
    public class User : BaseModel
    {
        [Required]
        public long NroIdentificacion { get; set; }

        [MaxLength(150)]
        [Required]
        public string Nombres { get; set; }

        [MaxLength(150)]
        [Required]
        public string Apellidos { get; set; }

        [MaxLength(150)]
        [Required]
        public string Correo { get; set; }

        [MaxLength(100)]
        public string Telefono { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public bool Activo { get; set; }
        
        public bool CambioPassword { get; set; }
    }
}
