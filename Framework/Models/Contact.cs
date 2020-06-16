using System.ComponentModel.DataAnnotations;

namespace Framework.Models
{
    public class Contact : BaseModel
    {
        [MaxLength(150)]
        [Required]
        public string Nombres { get; set; }

        public string Empresa { get; set; }

        [MaxLength(150)]
        [Required]
        public string Correo { get; set; }

        [MaxLength(100)]
        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Mensaje { get; set; }

    }
}
