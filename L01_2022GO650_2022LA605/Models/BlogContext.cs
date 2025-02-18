using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace L01_2022GO650_2022LA605.Models
{
   
        public class BlogContext : DbContext

        {
            public BlogContext(DbContextOptions<BlogContext> options) : base(options)
            {
            }

            public DbSet<Calificaciones> calificaciones { get; set; }
            public DbSet<Comentarioscs> comentarios { get; set; }  // <--- Agregar esta línea

            public DbSet<Usuarios> usuarios { get; set; }  // <--- Agregar esta línea


    
        }
}
