using System;

namespace Framework.Dtos
{
    public class ContactDto
    {
        public Guid Id { get; set; }

        public string Nombres { get; set; }
        
        public string Empresa { get; set; }
        
        public string Correo { get; set; }
        
        public string Telefono { get; set; }
        
        public string Mensaje { get; set; }

        public string Zona { get; set; }
    }
}
