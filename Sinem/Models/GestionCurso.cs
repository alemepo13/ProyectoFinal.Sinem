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
    [Table("GestionCurso")]
    public class GestionCurso
    {
        //propiedades automaticas para cada uno de los campos de la tabla
        [Key()]//indica que es la llave de la tabla
        [DisplayName("Id gestion curso: ")]//nombre que aparece en la pagina
        public int idGestionCurso { get; set; }
        [Required()]
        [DisplayName("Fecha de inicio: ")]
        public String fechaInicio { get; set; }
        [DisplayName("Fecha de finalizacion: ")]//nombre que aparece en la pagina
        public String fechaFinal { get; set; }
        [DisplayName("Fecha de registro: ")]//nombre que aparece en la pagina
        public DateTime fechaRegistro { get; set; }
        [DisplayName("Usuario crea:")]
        public string usuarioCrea { get; set; }
        [DisplayName("Fecha modifica: ")]//nombre que aparece en la pagina
        public DateTime fechaModifica { get; set; }
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; }

    }
}