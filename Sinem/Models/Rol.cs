using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sinem.Models
{
    [Table("Rol")]
    public class Rol
    {

        [Key()]
        public int idRol { get; set; }
        [Required()]
        [DisplayName("Nombre:")]
        public string nombre { get; set; }
        [DisplayName("descripcion:")]
        public string descripcion { get; set; }
        [DisplayName("Fecha de registro:")]
        public DateTime fechaRegistro { get; set; }
        [DisplayName("Usuario crea:")]
        public string usuarioCrea { get; set; }
        [DisplayName("Fecha modifica:")]
        public DateTime fechaModifica { get; set; }
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; }
    }
}