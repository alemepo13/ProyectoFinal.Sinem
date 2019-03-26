using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
//importaciones

namespace Sinem.Models
{
    [Table("GestionCurso")]
    public class GestionCurso
    {
        //propiedades automaticas para cada uno de los campos de la tabla
        [Key()]//indica que es la llave de la tabla
        [DisplayName("Id gestion curso: ")]//nombre que aparece en la pagina
        public int idGestionCurso { get; set; }
        //[Required()]
        [DisplayName("Fecha de inicio: ")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaInicio { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de finalizacion: ")]//nombre que aparece en la pagina
        [Required(ErrorMessage = "La  {0} es requerida")]
        public DateTime fechaFinal { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de registro: ")]//nombre que aparece en la pagina
        public DateTime fechaRegistro { get; set; }
        [DisplayName("Usuario crea:")]
        public string usuarioCrea { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha modifica: ")]//nombre que aparece en la pagina
        public DateTime fechaModifica { get; set; }
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; }
        [DisplayName("Cupo Maximo:")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public int cupo { get; set; }
        [DisplayName("Aula")]
        public int idAula { get; set; }
        [DisplayName("curso")]
        public int idCurso { get; set; }
        [DisplayName("horario")]
        public int idHorario { get; set; }
        [DisplayName("profesor")]
        public int idUsuario { get; set; }
    }

    public partial class GestionCurso2
    {
        public GestionCurso2(int idGestionCurso, DateTime fechaInicio, DateTime fechaFinal, DateTime fechaRegistro, 
            string usuarioCrea, DateTime fechaModifica, string usuarioModifica, int cupo, string numeroAula,
            string nombre, string descripcion, double costo, string dia, TimeSpan hora, string duracion, string nombrecompleto)
        {
            this.idGestionCurso = idGestionCurso;
            this.fechaInicio = fechaInicio;
            this.fechaFinal = fechaFinal;
            this.fechaRegistro = fechaRegistro;
            this.usuarioCrea = usuarioCrea;
            this.fechaModifica = fechaModifica;
            this.usuarioModifica = usuarioModifica;
            this.cupo2 = cupo;
            this.aula2 = numeroAula;
            this.curso2 = nombre;
            this.descripcion2 = descripcion;
            this.costo2 = costo.ToString();
            this.dia2 = dia.ToString();
            this.horario2 = hora.ToString();
            this.duracion2 = duracion.ToString();
            this.usuario2 = nombrecompleto;
        }

        [Key()]//indica que es la llave de la tabla
        [DisplayName("Codigo de curso: ")]//nombre que aparece en la pagina
        public int idGestionCurso { get; set; }
        [Required()]
        [DisplayName("Fecha de inicio: ")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaInicio { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de finalizacion: ")]//nombre que aparece en la pagina
        public DateTime fechaFinal { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de registro: ")]//nombre que aparece en la pagina
        public DateTime fechaRegistro { get; set; }
        [DisplayName("Usuario crea:")]
        public string usuarioCrea { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha modifica: ")]//nombre que aparece en la pagina
        public DateTime fechaModifica { get; set; }
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; }
        [DisplayName("Cupo Maximo:")]
        public int cupo2 { get; set; }
        [DisplayName("Aula:")]
        public string aula2 { get; set; }
        [DisplayName("Nombre del curso:")]
        public string curso2 { get; set; }
        [DisplayName("Descripcion:")]
        public string descripcion2 { get; set; }
        [DisplayName("Costo:")]
        public string costo2 { get; set; }
        [DisplayName("Dia:")]
        public string dia2 { get; set; }
        [DisplayName("Hora:")]
        public string horario2 { get; set; }
        [DisplayName("Duracion:")]
        public string duracion2 { get; set; }
        [DisplayName("Nombre del profesor:")]
        public string usuario2 { get; set; }

        public List<Vista_Asistencia> Estudiantes { get; set; }
    }

    public class CupoDelete
    {
        public int idGestionCurso { get; set; }
        public int idCupo { get; set; }
    }
}
