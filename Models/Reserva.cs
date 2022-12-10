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
        #region Constantes
        private const string CAMPO_REQUERIDO = "Este campo es requerido.";
        private const string ELEGIR_CANCHA = "Selecciona un tipo de cancha.";
        private const string RANGO_HORA_INICIO = "La hora de inicio no puede ser menor a 0 ni mayor a 23.";
        private const string RANGO_HORA_FIN = "La hora de fin no puede ser menor a 1 ni mayor a 24.";
        private const string RANGO_CANCHA = "Solo existen 3 canchas.";
        private const string RANGO_DNI = "El DNI debe contener 8 dígitos.";
        #endregion

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        [Range(00, 23, ErrorMessage = RANGO_HORA_INICIO)]
        public int HoraInicio { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        [Range(01, 24, ErrorMessage = RANGO_HORA_FIN)]
        public int HoraFin { get; set; }

        // [EnumDataType(typeof(TipoCancha))]
        [Required(ErrorMessage = ELEGIR_CANCHA)]
        //[ForeignKey("Cancha")]
        [Range(01, 03, ErrorMessage = RANGO_CANCHA)]
        public int Nro_cancha { get; set; }

        /* [ForeignKey("Usuario")]*/
        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        [MaxLength(8, ErrorMessage = RANGO_DNI)]
        [MinLength(8, ErrorMessage = RANGO_DNI)]
        public string Nro_Dni { get; set; }
    }
}
