using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Sinem.Models
{
    [Table("AsistenciaEstudiante")]
    public class AsistenciaEstudiante
    {
        [Key()]
        public int idAsistenciaEstudiante { get; set; }
        public int idGestionCurso { get; set; }
        public DateTime fecha { get; set; }
        public string asistio { get; set; }
        public string observaciones { get; set; }
        public DateTime fechaModifica { get; set; }
        public string usuarioModifica { get; set; }
        public int idUsuario { get; set; }

    }
}