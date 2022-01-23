using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midia_Indoo.DAL.DB
{
    public class DomainCtx : DbContext
    {
        public DomainCtx()
        {

        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Videos> Video { get; set; }
        public DbSet<Mensagem> Mensagem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS2014; Initial Catalog=Midia_Indoo-DB; User Id=sa; Password=12300456; MultipleActiveResultSets = true;", options => options.EnableRetryOnFailure());
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS2014; Initial Catalog=MediaIndoor; User Id=sa; Password=12300456; MultipleActiveResultSets = true;", options => options.EnableRetryOnFailure());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mensagem>().HasKey(l => l.Codigo);
            modelBuilder.Entity<Videos>().HasKey(l => l.Codigo);
            modelBuilder.Entity<Usuario>().HasKey(l => l.Codigo);
            modelBuilder.Entity<Player>().HasKey(l => l.Codigo);
   

#if DEBUG
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario()
                {
                    Codigo = 1,
                    Nome = "ADMIN",
                    Email = "ADMIN",
                    Senha = "1"
                });
#endif


        }
    }
}
