using BibliotecaVirtual.Web.Dados.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BibliotecaVirtual.Web.Dados
{
    public class DataContext:DbContext
    {
        public DataContext() : base("DataContext")
        {
        }

        public DbSet<LivroModel> Livros { get; set; }
        public DbSet<AutorModel> Autores { get; set; }
        public DbSet<AssuntoModel> Assuntos { get; set; }
        public DbSet<LivroAutorModel> LivroAutor { get; set; }
        public DbSet<LivroAssuntoModel> LivroAssunto { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}