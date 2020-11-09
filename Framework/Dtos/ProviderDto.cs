using System;

namespace Framework.Dtos
{
    public class ProviderDto
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Nit { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string Correo { get; set; }

        public string Observaciones { get; set; }
    }
}
