using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sinem.Models
{
    
    [Table("AsistenciaProfesor")]
    public class AsistenciaProfesor
    {

        [Key()]
            public int idAsistenciaProfesor { get; set; }
            public int idGestionCurso { get; set; }
            public DateTime Fecha { get; set; }
            public string asistio { get; set; }
            public string observaciones { get; set; }
            public DateTime fechaModifica { get; set; }
            public string usuarioModifica { get; set; }
            public int idUsuario { get; set; }
    }

 }
