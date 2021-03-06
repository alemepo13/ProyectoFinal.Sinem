﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel;
//importaciones

namespace Sinem.Models
{
    
    [Table("AsistenciaProfesor")]
    public class AsistenciaProfesor
    {
        //propiedades automaticas para cada uno de los campos de la tabla
        [Key()]//indica que es la llave de la tabla
        public int idAsistenciaProfesor { get; set; }
        public int idGestionCurso { get; set; }

        [DisplayName("Fecha:")]//nombre que aparece en la pagina
        public DateTime Fecha { get; set; }
        [DisplayName("Asistio:")]//nombre que aparece en la pagina
        public string asistio { get; set; }
        [DisplayName("Observaciones")]//nombre que aparece en la pagina
        [RegularExpression("^[-_ ,A-Za-z0-9]*$", ErrorMessage = "Caracteres no permitidos")] // Acepta letras, numeros, guion y espacio.
        public string observaciones { get; set; }

       
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [DisplayName("Fecha modifica:")]
        public DateTime fechaModifica { get; set; }
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; }



        public int idUsuario { get; set; }
    }

 }
