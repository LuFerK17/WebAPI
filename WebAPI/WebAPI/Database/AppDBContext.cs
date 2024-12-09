using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Database
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; } //sirve para darle el comportamiento de los datos de la tabla

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>()
                .HasKey(e => e.IdPersona); //especifico quien es la llave primaria de la tabla 
        }
    }
}
