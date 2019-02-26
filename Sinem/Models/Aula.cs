using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//importaciones

namespace Sinem.Models
{
    [Table("Aula")]
    public class Aula
    {
        //propiedades automaticas para cada uno de los campos de la tabla
        [Key()]//indica que es la llave de la tabla
        [DisplayName("Id del Aula:")]
        public int idAula { get; set; }
        //[Required()]//indica que es un campo requerido
        [DisplayName("Numero de aula:")]//nombre que aparece en la pagina
        [Required(ErrorMessage = "El {0} es requerido")]//validacion del numero de aula
        [Remote("NumAula", "Aulas", ErrorMessage = "El numero de aula ya está en uso")]//aqui se puede ver la validacion del numero de aula si ya existe
        public string numeroAula { get; set; }
        [DisplayName("Tipo de aula:")]
        [Required(ErrorMessage = "El {0} es requerido")]//validacion de tipo aula
        public string tipoAula { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de registro:")]
        public DateTime fechaRegistro { get; set; }
        [DisplayName("Usuario crea:")]
        public string usuarioCrea { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [DisplayName("Fecha modifica:")]//nombre que aparece en la pagina
        public DateTime fechaModifica { get; set; }
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; } 
    }
}