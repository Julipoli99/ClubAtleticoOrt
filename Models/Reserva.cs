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
        private const string CAMPO_REQUERIDO = "Este campo es requerido.";
        private const string ELEGIR_CANCHA = "Selecciona un tipo de cancha.";

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        public int HoraInicio { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        public int HoraFin { get; set; }

        // [EnumDataType(typeof(TipoCancha))]
        [Required(ErrorMessage = ELEGIR_CANCHA)]
        [ForeignKey("Cancha")]
        public int id_cancha { get; set; }

        /* [ForeignKey("Usuario")]*/
        public int id_usuario { get; set; }
    }
}
