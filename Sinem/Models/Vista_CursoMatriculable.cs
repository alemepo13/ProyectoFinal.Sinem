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
}