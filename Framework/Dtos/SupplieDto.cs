using System;

namespace Framework.Dtos
{
    public class SupplieDto
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Categoria { get; set; }

        public int CantidadMinima { get; set; }

        public int CantidadMaxima { get; set; }

        public bool TieneIva { get; set; }
    }
}
