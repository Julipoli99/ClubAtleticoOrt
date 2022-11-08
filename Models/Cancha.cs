using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAtleticoOrt.Models
{
    public class Cancha
    {
        public int Id { get; set; }

        [EnumDataType(typeof(TipoCancha))] 
        public TipoCancha Tipo { get; set; }

        public int Estado { get; set; }


    }
}
