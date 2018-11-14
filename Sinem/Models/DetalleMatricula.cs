using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sinem.Models
{
    [Table("DetalleMatricula")]
    public class DetalleMatricula
    {
        //propiedades automaticas para cada uno de los campos de la tabla
        [Key()]//indica que es la llave de la tabla
        [DisplayName("Id gestion curso: ")]//nombre que aparece en la pagina
        public int idDetalleMatricula { get; set; }
        public double costo { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de registro: ")]//nombre que aparece en la pagina
        public DateTime fechaRegistro { get; set; }
        [DisplayName("Usuario crea:")]
        public string usuarioCrea { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [DisplayName("Fecha modifica: ")]//nombre que aparece en la pagina
        public DateTime fechaModifica { get; set; }
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; }

        [DisplayName("Estudiante")]
        public int idEstudiante { get; set; }
        [DisplayName("curso")]
        public int idCurso { get; set; }


        [DisplayName("Profesor")]
        public int idProfesor { get; set; }
        [DisplayName("Horario")]
        public int idHorario { get; set; }


        [DisplayName("GestionCurso")]
        public int idgestionCurso { get; set; }

    }
}