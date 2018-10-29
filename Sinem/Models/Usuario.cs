using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sinem.Models
{
        [Table("Usuario")]
        public class Usuario
        {
            [Key()]
            public int idUsuario { get; set; }
            public int idDireccion { get; set; }
            [Required()]
            [DisplayName("Cédula:")]
            public string cedula { get; set; }
            [DisplayName("Nombre:")]
            public string nombre { get; set; }
            [DisplayName("Apellidos:")]
            public string apellido { get; set; }
            [DisplayName("Teléfono:")]
            public int telefono { get; set; }
            [DisplayName("Correo:")]
            public string correo { get; set; }
            [DisplayName("Fecha de nacimiento:")]
            public DateTime fechaNacimiento { get; set; }
            [DisplayName("Usuario:")]
            public string usuario { get; set; }
            [DisplayName("Contraseña:")]
            public string contraseña { get; set; }
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

