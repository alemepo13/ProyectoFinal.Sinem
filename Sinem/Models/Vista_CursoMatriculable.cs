using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sinem.Models
{
   
    public class Vista_CursoMatriculable
    {
        public int idGC { get; set; }
        public DateTime FechaInicio { get; internal set; }
        public DateTime FechaFinal { get; internal set; }
        public string Aula { get; internal set; }
        public string Horario { get; internal set; }
        public string Curso { get; internal set; }
        public int Cupo { get; set; }
        public bool YaMatriculado { get; set; }
        public bool horarioOcupado { get; set; }
        public DateTime fechaActual { get; internal set; }
        public string nombreE { get; internal set; }
        public string cedula { get; internal set; }
        public string profesor { get; internal set; }
    }

    public class Vista_CursoMatriculado
    {
        public int idGC { get; set; }
        public DateTime FechaInicio { get; internal set; }
        public DateTime FechaFinal { get; internal set; }
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
        public int idGestionCurso { get; internal set; }
    }
}