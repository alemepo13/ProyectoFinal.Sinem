using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sinem.Models
{
    [Table("Cupo")]
    public class Cupo
    {
        [Key()]//indica que es la llave de la tabla
        public int idCupo { get; set; }
        public int cupo { get; set; }
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


        [DisplayName("GestionCurso")]
        public int idGestionCurso { get; set; }
    }
}