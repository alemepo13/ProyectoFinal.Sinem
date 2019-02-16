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
        [Required(ErrorMessage ="Indica correctamente su provincia")]// valida su pronvincia 
        [DisplayName("Provincia:")]//nombre que aparece en la pagina
        public string provincia { get; set; }
        [DisplayName("Distrito:")]
        [Required(ErrorMessage = "Indica correctamente su distrito")]//valida su distrito
        public string distrito { get; set; }
        [DisplayName("Canton:")]
        [Required(ErrorMessage = "Indica correctamente su canton")]//valida su canton
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
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; }
        [NotMapped()]
        public string descripcion { get { return $"{provincia}, {canton}, {distrito}"; } }
    }
}