using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAtleticoOrt.Models
{
    public class Cancha
    {
        [Key]
        public int Id { get; set; }

      //  [EnumDataType(typeof(TipoCancha))] 
       // public TipoCancha Tipo { get; set; }

        /* [EnumDataType(typeof(Estado))]
         public Estado Estado { get; set; }*/

        public string Descripcion { get; set; }

    }
}
