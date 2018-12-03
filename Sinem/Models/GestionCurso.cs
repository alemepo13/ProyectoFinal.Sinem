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
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [DisplayName("Fecha modifica: ")]//nombre que aparece en la pagina
        public DateTime fechaModifica { get; set; }
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; }
        [DisplayName("Aula")]
        public int idAula { get; set; }
        [DisplayName("curso")]
        public int idCurso { get; set; }
        [DisplayName("horario")]
        public int idHorario { get; set; }
        [DisplayName("profesor")]
        public int idUsuario { get; set; }

    }
}
