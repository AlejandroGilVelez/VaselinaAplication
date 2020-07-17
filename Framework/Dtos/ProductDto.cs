using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }
      
        public string Descripcion { get; set; }
        
        public int Peso { get; set; }

        public string Imagen { get; set; }

    }
}
