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
        //[Required()]//indica que es un campo requerido
        [DisplayName("Nombre:")]//nombre que aparece en la pagina
        [MaxLength(20,ErrorMessage ="El nombre del curso debe de ser menor a 20 caracteres")]
        [RegularExpression ("^[A-Za-z][-_ ,A-Za-z0-9]*$", ErrorMessage ="Caracteres no permitidos")]// Acepta letras, numeros, guion y espacio.
        [Required(ErrorMessage = "El {0} es requerido")]//validacion de nombre
        public string nombre { get; set; }
        [DisplayName("Descripción:")]
        [RegularExpression("^[-_ ,A-Za-z0-9]*$", ErrorMessage = "Caracteres no permitidos")] // Acepta letras, numeros, guion y espacio.
        [Required(ErrorMessage = "La {0} es requerida")] //validacion de la descripcion
        public string descripcion { get; set; }
        [DisplayName("Costo:")]
        [Required(ErrorMessage = "El {0} es requerido")] //validacion de costo
        [Range(0, 15000, ErrorMessage = "El campo {0} debe ser un numero entre {1} y {2}")]//validacion para ver cual es el rango del costo o el maximo
        public double costo { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de registro:")]
        public DateTime fechaRegistro { get; set; }
        [DisplayName("Usuario crea:")]//nombre que aparece en la pagina
        public string usuarioCrea { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha modifica:")]
        public DateTime fechaModifica{ get; set; }
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; }



    }
}