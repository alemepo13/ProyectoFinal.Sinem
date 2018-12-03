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
    [Table("Usuario")]
    public class Usuario
    {
        //propiedades automaticas para cada uno de los campos de la tabla
        [Key()]//indica que es la llave de la tabla
        public int idUsuario { get; set; }
        public int idDireccion { get; set; }
        [Required()]//indica que es un campo requerido
        [DisplayName("Cédula:")]//nombre que aparece en la pagina
        public string cedula { get; set; }
        [DisplayName("Nombre:")]
        public string nombre { get; set; }
        [DisplayName("Apellidos:")]
        public string apellido { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Teléfono:")]//nombre que aparece en la pagina
             public int telefono { get; set; }
            [DisplayName("Correo:")]
            public string correo { get; set; }
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
            [DisplayName("Fecha de nacimiento:")]
            public DateTime fechaNacimiento { get; set; }
            [DisplayName("Usuario:")]
            public string usuario { get; set; }
            [DisplayName("Contraseña:")]//nombre que aparece en la pagina
            public string contraseña { get; set; }
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
            

        [NotMapped()]
        public string nombrecompleto { get { return $"{nombre}, {apellido}"; } }

        [DisplayName ("Rol")]
        [NotMapped()]
        public int idRol { get; set;  }
    }
}

