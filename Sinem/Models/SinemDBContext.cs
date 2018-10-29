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
        public SinemDBContext() : base("Server=localhost;Database=bd_sinem;Uid=root;Pwd=1234; port=3307")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Aula> Aulas { get; set; }
        //DbSets....

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<GestionCurso> GestionCursos { get; set;}

        public DbSet<Permiso>Permisos { get; set; }
        public DbSet<Rol> Roles { get; set; }
    }
}