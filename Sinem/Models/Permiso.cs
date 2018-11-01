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
    [Table("Permiso")]
    public class Permiso
    {
        //propiedades automaticas para cada uno de los campos de la tabla
        [Key, Column(Order = 0)]//indica que es la llave de la tabla
        public int idUsuario { get; set; }
        [Key, Column(Order = 1)]//indica que es la llave de la tabla
        public int idRol { get; set; }
    }
}