using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sinem.Models
{
    [Table("GestionCurso")]
    public class GestionCurso
    {
        [Key()]
        [DisplayName("Id gestion curso: ")]
        public int idGestionCurso { get; set; }
        [Required()]
        [DisplayName("Fecha de inicio: ")]
        public String fechaInicio { get; set; }
        [DisplayName("Fecha de finalizacion: ")]
        public String fechaFinal { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string usuarioCrea { get; set; }
        public DateTime fechaModifica { get; set; }
        public string usuarioModifica { get; set; }

    }
}