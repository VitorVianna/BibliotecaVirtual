using System;
using BibliotecaVirtual.Web.Dados.Models;
using BibliotecaVirtual.Web.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BibliotecaVirtual.Test
{
    [TestClass]
    public class BibliotecaVirtualTests
    {
        [TestMethod]
        public void AdicionarLivros()
        {
            var NovoLivro = new LivroModel()
            {
                Titulo = "A Divina Comédia",
                AnoPublicacao = "2020",
                Edicao = 1,
                Editora = "Principis",
                Valor = 37.90m
            };

            LivroService.CriarLivro(NovoLivro);

            var NovoAutor = AutorService.criarAutor("Dante Alighieri");
            var NovoAssunto = AssuntoService.criarAssunto("Romance");

            LivroAutorService.VincularLivroAutor(NovoLivro, NovoAutor);
            LivroAssuntoService.VincularLivroAssunto(NovoLivro, NovoAssunto);
        }

        [TestMethod]
        public void AdicionarVariasCategorias()
        {
            string[] categorias = new string[] 
            {
                "Autobiografia",
                "Biografia",
                "Fantasia",
                "Horror",
                "Literatura",
                "Novelas",
                "Comédia",
                "Suspense",
                "Vampirismo",
                "Auto-Ajuda",
                "Negócios",
                "Religioso",
                "Aventura",
                "Guerra",
                "Romance"
            };

            foreach (var cat in categorias)
            {
                AssuntoService.criarAssunto(cat);
            }
        }
    }
}
