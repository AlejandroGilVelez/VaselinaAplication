using System.ComponentModel.DataAnnotations;

namespace Framework.Models
{
    public class Client : BaseModel
    {
        [MaxLength(150)]
        [Required]
        public string Nit { get; set; }

        [MaxLength(150)]
        [Required]
        public string Nombres { get; set; }

        [MaxLength(150)]
        [Required]
        public string Apellidos { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string Correo { get; set; }

        public string Ciudad { get; set; }

        public string Pais { get; set; }

        public string Zona { get; set; }
    }
}
