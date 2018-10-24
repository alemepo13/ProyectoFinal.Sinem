using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sinem.Models
{
    [Table("Curso")]
    public class Curso
    {
        [Key()]
        public int idCurso { get; set; }
        [Required()]
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public double costo { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string usuarioCrea { get; set; }
        public DateTime fechaModifica{ get; set; }
        public string usuarioModifica { get; set; }
    }
}