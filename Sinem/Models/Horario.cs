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
    [Table("Horario")]
    public class Horario
    {
        //propiedades automaticas para cada uno de los campos de la tabla
        [Key()]//indica que es la llave de la tabla
        public int idHorario { get; set; }
        //[Required()]//indica que es un campo requerido
        [DisplayName("Día:")]//nombre que aparece en la pagina
        [Required(ErrorMessage = "La {0} es requerido")]
        public string dia { get; set; }
        [DisplayName("Hora:")]
        [Required(ErrorMessage = "La {0} es requerida")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:hh\\:mm}")]
        [Range(typeof(TimeSpan),"00:00","23:59")]
        public System.TimeSpan hora { get; set; }
        [DisplayName("Tiempo de duración:")]
        [Required(ErrorMessage = "La {0} es requerido")]
        public string tiempoDuracion { get; set; }
        [DisplayName("Tipo de duracion")]
        public string tipo { get; set; }
        public string estado { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de registro:")]//nombre que aparece en la pagina
        public DateTime fechaRegistro { get; set; }
        [DisplayName("Usuario crea:")]
        public string usuarioCrea { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha modifica:")]
        public DateTime fechaModifica { get; set; }
        [DisplayName("Usuario modifica:")]
        public string usuarioModifica { get; set; }
        [NotMapped()]
        public string descripcion { get { return $"{dia}, {hora}"; } }
    }
}