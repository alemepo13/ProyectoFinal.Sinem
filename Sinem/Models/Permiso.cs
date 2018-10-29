﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sinem.Models
{
    [Table("Permiso")]
    public class Permiso
    {
        [Key, Column(Order = 0)]
        public int idUsuario { get; set; }
        [Key, Column(Order = 1)]
        public int idRol { get; set; }
    }
}