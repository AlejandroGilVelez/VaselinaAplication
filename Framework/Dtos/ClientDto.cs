﻿using System;

namespace Framework.Dtos
{
    public class ClientDto
    {
        public Guid Id { get; set; }

        public string Nit { get; set; }
       
        public string Nombres { get; set; }
       
        public string Apellidos { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string Correo { get; set; }

        public string Ciudad { get; set; }

        public string Pais { get; set; }

        public string Zona { get; set; }

        public string Contacto { get; set; }

        public string Observaciones { get; set; }
    }
}
