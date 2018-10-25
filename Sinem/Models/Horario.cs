using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sinem.Models
{
    [Table("Horario")]
    public class Horario
    {
        [Key()]
        public int idHorario { get; set; }
        [Required()]
        [DisplayName("Día::")]
        public string dia { get; set; }
        [DisplayName("Hora:")]
        public System.TimeSpan hora { get; set; }
        [DisplayName("Tiempo de duración:")]
        public string tiempoDuracion { get; set; }
        [DisplayName("Fecha de registro:")]
        public DateTime fechaRegistro { get; set; }
        [DisplayName("Usuario crea:")]
        public string usuarioCrea { get; set; }
        [DisplayName("Fecha modifica:")]
        public DateTime fechaModifica { get; set; }
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; }
    }
}