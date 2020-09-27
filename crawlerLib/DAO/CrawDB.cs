using crawlerLib.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace crawlerLib.DAO
{
    public class CrawDB : DbContext
    {
        public DbSet<Noticia> Noticias { get; set; }

        public CrawDB()
        {
            iniciar();
        }

        private void iniciar()
        {
            this.Database.EnsureCreated();
            Console.WriteLine("Banco iniciado");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseOracle("Data Source=localhost;User Id=C##JOAO;Password=123;");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Noticia>()
                .HasIndex(n => n.Link)
                .IsUnique();
        }
    }
}
