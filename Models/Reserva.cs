using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [EnumDataType(typeof(TipoCancha))]
        public TipoCancha id_cancha { get; set; }
    }
}
