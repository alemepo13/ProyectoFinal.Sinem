using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
//importaciones

namespace Sinem.Models
{
   [DbConfigurationType(typeof(MySqlEFConfiguration))]//especifica el tipo de conexion, en este caso mysql
    public class SinemDBContext : DbContext
    {//metodo para realizar la conexion con la base de datos
        public SinemDBContext() : base("Server=localhost;Database=bd_sinem;Uid=root;Pwd=1234;")//realiza la conexion
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        //selecciona las tablas de la DB
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Permiso>Permisos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        
        public DbSet<AsistenciaEstudiante> AsistenciaEstudiantes { get; set; }
        public DbSet<AsistenciaProfesor> AsistenciaProfesores { get; set; }
        public DbSet<GestionCurso> GestionCursos { get; set; }
        public DbSet<Salario> Salarios { get; set; }


     
    }
}