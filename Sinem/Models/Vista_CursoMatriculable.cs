using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sinem.Models
{
    public class Vista_CursoMatriculable
    {
        public int idGC { get; set; }

        public string FechaInicio { get; internal set; }
        public string FechaFinal { get; internal set; }
        public string Aula { get; internal set; }
        public string Horario { get; internal set; }
        public string Curso { get; internal set; }

    }
    public class Vista_CursoMatriculado
    {
        public int idGC { get; set; }
        public string FechaInicio { get; internal set; }
        public string FechaFinal { get; internal set; }
        public string Aula { get; internal set; }
        public string Horario { get; internal set; }
        public string Curso { get; internal set; }
    }
    public class Vista_Asistencia {
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string observaciones { get; set; }
        public Boolean asistio { get; set; }
        public int idUsuario { get; set; }
    }
}