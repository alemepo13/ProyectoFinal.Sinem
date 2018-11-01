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
    [Table("Curso")]
    public class Curso
    {
        //propiedades automaticas para cada uno de los campos de la tabla
        [Key()]//indica que es la llave de la tabla
        public int idCurso { get; set; }
        [Required()]//indica que es un campo requerido
        [DisplayName("Nombre:")]//nombre que aparece en la pagina
        public string nombre { get; set; }
        [DisplayName("Descripción:")]
        public string descripcion { get; set; }
        [DisplayName("Costo:")]
        public double costo { get; set; }
        [DisplayName("Fecha de registro:")]
        public DateTime fechaRegistro { get; set; }
        [DisplayName("Usuario crea:")]//nombre que aparece en la pagina
        public string usuarioCrea { get; set; }
        [DisplayName("Fecha modifica:")]
        public DateTime fechaModifica{ get; set; }
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; }
    }
}