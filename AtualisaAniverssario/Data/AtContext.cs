using Microsoft.EntityFrameworkCore;
using AtualisaAniverssario.Models;


namespace AtualisaAniverssario.Data
{
    public partial class AtContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=192.168.0.5;User Id=root;Password=;Database=Ferramentas");
            }
        }

        public AtContext(DbSet<Usuarios> usuarios)
        {
            Usuarios = usuarios;
        }

        public AtContext()
        {
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{ 

        //}

        public  DbSet<Usuarios> Usuarios { get; set; }

        
    }
}
