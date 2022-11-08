using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAtleticoOrt.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int HoraInicio { get; set; }

        public int HoraFin { get; set; }
    }
}
