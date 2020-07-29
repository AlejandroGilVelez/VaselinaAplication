using System;

namespace Framework.Dtos
{
    public class ClientDto
    {
        public Guid Id { get; set; }

        public string Nit { get; set; }
       
        public string Nombre { get; set; }
       
        public string Apellidos { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string Correo { get; set; }

        public string Ciudad { get; set; }

        public string Pais { get; set; }
    }
}
