namespace Framework.Models
{
    public class CambioPassword : BaseModel
    {
        public User Usuario { get; set; }

        public int  MinutosExpiracion { get; set; }

        public bool Activo { get; set; }
    }
}
