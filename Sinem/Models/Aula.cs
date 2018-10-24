using System;
using System.Collections.Generic;
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
        public string numeroAula { get; set; }
        public string tipoAula { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string usuarioCrea { get; set; }
        public DateTime fechaModifica { get; set; }
        public string usuarioModifica { get; set; } 
    }
}