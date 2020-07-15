using System.ComponentModel.DataAnnotations;

namespace Framework.Models
{
    public class Product : BaseModel
    {
        [MaxLength(150)]
        [Required]
        public string Nombre { get; set; }

        [MaxLength(150)]
        [Required]
        public string Descripcion { get; set; }


        [Required]
        public int Peso { get; set; }

        [Required]
        public string Imagen { get; set; }
    }
}
