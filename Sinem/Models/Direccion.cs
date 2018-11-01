using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sinem.Models
{
    [Table("Direccion")]
    public class Direccion
    {
        //propiedades automaticas para cada uno de los campos de la tabla
        [Key()]//indica que es la llave de la tabla
        public int idDireccion { get; set; }
        [Required()]//indica que es un campo requerido
        [DisplayName("Provincia:")]//nombre que aparece en la pagina
        public string nombre { get; set; }
        [DisplayName("Distrito:")]
        public string descripcion { get; set; }
        [DisplayName("Canton:")]
        public string canton { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de registro:")]
        public DateTime fechaRegistro { get; set; }
        [DisplayName("Usuario crea:")]//nombre que aparece en la pagina
        public string usuarioCrea { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [DisplayName("Fecha modifica:")]
        public DateTime fechaModifica { get; set; }
    }
}