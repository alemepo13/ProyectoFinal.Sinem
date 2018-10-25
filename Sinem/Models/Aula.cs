using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sinem.Models
{
    [Table("Aula")]
    public class Aula
    {
        [Key()]
        public int idAula { get; set; }
        [Required()]
        [DisplayName("Numero de aula:")]
        public string numeroAula { get; set; }
        [DisplayName("Tipo de aula:")]
        public string tipoAula { get; set; }
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