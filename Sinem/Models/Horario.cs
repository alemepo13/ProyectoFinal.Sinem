using System;
using System.Collections.Generic;
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
        public string dia { get; set; }
        public System.TimeSpan hora { get; set; }
        public string tiempoDuracion { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string usuarioCrea { get; set; }
        public DateTime fechaModifica { get; set; }
        public string usuarioModifica { get; set; }
    }
}