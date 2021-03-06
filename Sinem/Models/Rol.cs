﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
//importaciones

namespace Sinem.Models
{
    [Table("Rol")]
    public class Rol
    {
        //propiedades automaticas para cada uno de los campos de la tabla
        [Key()]//indica que es la llave de la tabla
        public int idRol { get; set; }
        [Required(ErrorMessage ="digita su nombre segun se indica")]//Valida su nombre correctamente
        [DisplayName("Nombre:")]//nombre que aparece en la pagina
        [RegularExpression("^[-_ ,A-Za-z0-9]*$", ErrorMessage = "Caracteres no permitidos")] // Acepta letras, numeros, guion y espacio.
        public string nombre { get; set; }
        [Required(ErrorMessage = "Indica una descripcion")]//Valida una descripcion
        [DisplayName("descripcion:")]
        [RegularExpression("^[-_ ,A-Za-z0-9]*$", ErrorMessage = "Caracteres no permitidos")] // Acepta letras, numeros, guion y espacio.
        public string descripcion { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de registro:")]//nombre que aparece en la pagina
        public DateTime fechaRegistro { get; set; }
        [DisplayName("Usuario crea:")]
        public string usuarioCrea { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [DisplayName("Fecha modifica:")]//nombre que aparece en la pagina
        public DateTime fechaModifica { get; set; }
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; }
    }
}