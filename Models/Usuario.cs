using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAtleticoOrt.Models
{
    public class Usuario
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Contraseña { get; set; }
        public DateTime FechaInscripto { get; set; }

        public string Telefono { get; set; }
    }
}
