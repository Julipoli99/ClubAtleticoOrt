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
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public int HoraInicio { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public int HoraFin { get; set; }

        // [EnumDataType(typeof(TipoCancha))]
        [Required(ErrorMessage = "Selecciona un tipo de cancha.")]
        [ForeignKey("Cancha")]
        public int id_cancha { get; set; }

       /* [ForeignKey("Usuario")]
        public int id_usuario { get; set; }*/
    }
}
