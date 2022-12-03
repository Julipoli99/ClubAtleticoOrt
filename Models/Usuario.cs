using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAtleticoOrt.Models
{
    public class Usuario
    {
        internal readonly StringValues campo;
        private const CAMPO_REQUERIDO = "Este campo es requerido.";
        private const FORMATO_INVALIDO = "Formato invalido";
        private const CARACTERES_MAX = "El nombre no puede tener mas de 20 caracteres";
        private const CARACTERES_MIN = "El nombre no puede tener menos de 2 caracteres";
        private const CARACTERES_MIN = "El nombre no puede tener menos de 2 caracteres";
        private const MAYOR_CONTRASEÑA = "Por su seguridad, proporcione una contraseña con mas caracteres";

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        public int Id { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        public string Dni { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        [MaxLength(20, ErrorMessage = CARACTERES_MAX)]
        [MinLength(2, ErrorMessage = CARACTERES_MIN)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        [EmailAddress(ErrorMessage = FORMATO_INVALIDO)]
        //[RegularExpression(@"/^\w + ([\.-] ?\w +)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/", ErrorMessage = "Formato invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        [MinLength(2, ErrorMessage = MAYOR_CONTRASEÑA)]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        public DateTime FechaInscripto { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
       // [RegularExpression(@"= /^\d{10}$/", ErrorMessage = "Formato invalido")]
        [Phone(ErrorMessage = FORMATO_INVALIDO)]
        public string Telefono { get; set; }
    }
}
