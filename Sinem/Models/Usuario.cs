﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;



//importaciones

namespace Sinem.Models
{
    [Table("Usuario")]
    public class Usuario
    {

        

        //propiedades automaticas para cada uno de los campos de la tabla
        [Key()]//indica que es la llave de la tabla
        [DisplayName("Id Usuario:")]
        public int idUsuario { get; set; }
        [DisplayName("Id Direccion:")]
        public int idDireccion { get; set; }
        //[Required()]//indica que es un campo requerido
        [DisplayName("Cédula:")]//nombre que aparece en la pagina
        [Required(ErrorMessage = "La {0} es requerida")]//aqui se puede ver la vilidacion de cedula
        [Remote("Cedulas", "Usuarios", ErrorMessage = "La cedula ya está en uso")]//aqui se verifica si la cedula ya esta en uso
        public string cedula { get; set; }
        [DisplayName("Nombre:")]
        [Required(ErrorMessage = "El {0} es requerido")]//aqui se puede ver la validacion de nombre
        public string nombre { get; set; }
        [Required(ErrorMessage = "Los {0} son requeridos")] //aqui se puede ver la validacion de apellidos
        [DisplayName("Apellidos:")]
        public string apellido { get; set; }
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Teléfono:")]//nombre que aparece en la pagina
        [Required(ErrorMessage = "El {0} es requerido")]//validacion de telefono
        //[StringLength(15, MinimumLength = 8, ErrorMessage = "El campo {0} debe de tener una longitud minima de {2} y una longitud maxima {1}")] // hay que hacer un updated database
        [Remote("Telefonos", "Usuarios", ErrorMessage = "Este telefono ya está en uso")] //validacion de telefono que ya esta en uso
        public int telefono { get; set; }
        [DisplayName("Correo:")]
        [Required(ErrorMessage = "El {0} es requerido")] //aqui se puede ver la validacion de correo
        [EmailAddress(ErrorMessage = "El {0} no tiene el formato correcto")]//aqui se puede ver si el correo tiene el formato adecuado
        [Remote("Correos", "Usuarios", ErrorMessage = "Este Correo ya está en uso")]//aqui se verifica si el correo ya esta en uso
        public string correo { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de nacimiento:")]
        [Required(ErrorMessage = "La {0} es requerida")]//aqui se puede ver la validacion de fecha de nacimiento
        [validarFecha]
        public DateTime fechaNacimiento { get; set; }

        [DisplayName("Usuario:")]
        [Required(ErrorMessage = "El {0} es requerido")]//aqui se puede ver la validacion de usuario

        [Remote("IsUserNameAvailable", "Usuarios", ErrorMessage = "Este nombre de usuario ya está en uso.")]//aqui en esta validacion se puede ver si el usuario ya existe
        public string usuario { get; set; }


        [DisplayName("Contraseña:")]//nombre que aparece en la pagina
        [Required(ErrorMessage = "La {0} es requerida")]
        public string contraseña { get; set; }
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
            [DisplayName("Fecha de registro:")]
            public DateTime fechaRegistro { get; set; }
            [DisplayName("Usuario crea:")]
            public string usuarioCrea { get; set; }
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
            
            [DisplayName("Fecha modifica:")]//nombre que aparece en la pagina
            public DateTime fechaModifica { get; set; }
            
            [DisplayName("Usuario modifica:")]
            public string usuarioModifica { get; set; }
            

        [NotMapped()]
        public string nombrecompleto { get { return $"{nombre}, {apellido}"; } }

        [DisplayName ("Rol")]
        [NotMapped()]
        public int idRol { get; set;  }
    }

    public class PermisoDelete
    {
        public int idUsuario { get; set; }
        public int idRol { get; set; }
    }
}

