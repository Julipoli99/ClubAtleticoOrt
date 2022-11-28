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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required(ErrorMessage = "Este campo es requerido.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [MaxLength(20, ErrorMessage = "El nombre no puede tener mas de 20 caracteres")]
        [MinLength(2, ErrorMessage = "El nombre no puede tener menos de 2 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [EmailAddress(ErrorMessage ="Formato invalido")]
        //[RegularExpression(@"/^\w + ([\.-] ?\w +)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/", ErrorMessage = "Formato invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [MinLength(2, ErrorMessage = "Por su seguridad, proporcione una contraseña con mas caracteres")]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public DateTime FechaInscripto { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
       // [RegularExpression(@"= /^\d{10}$/", ErrorMessage = "Formato invalido")]
        [Phone(ErrorMessage = "Formato invalido")]
        public string Telefono { get; set; }
    }
}
