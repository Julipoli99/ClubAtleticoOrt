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

        #region Constantes
        private const string CAMPO_REQUERIDO = "Este campo es requerido.";
        private const string FORMATO_EMAIL_INVALIDO = "El email ingresado tiene un formato inválido";
        private const string FORMATO_TELEFONO_INVALIDO = "El telefono ingresado tiene un formato inválido";
        private const string RANGO_CARACTERES = "El nombre puede tener entre 2 y 20 caracteres";
        private const string MAYOR_CONTRASEÑA = "Por su seguridad, proporcione una contraseña con más caracteres";
        private const string RANGO_DNI = "El DNI debe contener 8 dígitos y solo pueden ser numéricos.";
        private const string FORMATO_NOMBRE_APELLIDO_INVALIDO = "El nombre y el apellido no pueden contener números ni caracteres especiales"; 
        #endregion

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        public int Id { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO), MinLength(8, ErrorMessage = RANGO_DNI), MaxLength(8, ErrorMessage = RANGO_DNI)]
        [RegularExpression(@"[0-9]{1,9}(\.[0-9]{0,2})?$", ErrorMessage = RANGO_DNI)]
        public string Dni { get; set; }
        

        [Required(ErrorMessage = CAMPO_REQUERIDO), MinLength(2, ErrorMessage = RANGO_CARACTERES), MaxLength(20, ErrorMessage = RANGO_CARACTERES)]
        [RegularExpression(@"[a-zA-ZñÑ\s]{2,50}", ErrorMessage = FORMATO_NOMBRE_APELLIDO_INVALIDO)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO), MinLength(2, ErrorMessage = RANGO_CARACTERES), MaxLength(20, ErrorMessage = RANGO_CARACTERES)]
        [RegularExpression(@"[a-zA-ZñÑ\s]{2,50}", ErrorMessage = FORMATO_NOMBRE_APELLIDO_INVALIDO)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        [EmailAddress(ErrorMessage = FORMATO_EMAIL_INVALIDO)]
        public string Email { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        [MinLength(2, ErrorMessage = MAYOR_CONTRASEÑA)]
        
        public string Contraseña { get; set; }

        [Required(ErrorMessage = CAMPO_REQUERIDO)]
        public DateTime FechaInscripto { get; set; }
        
        [Required(ErrorMessage = CAMPO_REQUERIDO), MinLength(6, ErrorMessage = RANGO_DNI), MaxLength(10, ErrorMessage = RANGO_DNI)]
        [RegularExpression(@"[0-9]{6,10}", ErrorMessage = FORMATO_TELEFONO_INVALIDO)]
        public string Telefono { get; set; }
    }
}
