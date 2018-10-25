using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sinem.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class SinemDBContext : DbContext
    {
        public SinemDBContext() : base("Server=localhost;Database=bd_sinem;Uid=root;Pwd=Zx8G7nT5f45000V*;")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Aula> Aulas { get; set; }
        //DbSets....

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Horario> Horarios { get; set; }

        public System.Data.Entity.DbSet<Sinem.Models.AsistenciaProfesor> AsistenciaProfesors { get; set; }

        public System.Data.Entity.DbSet<Sinem.Models.AsistenciaEstudiante> AsistenciaEstudiantes { get; set; }
    }
}