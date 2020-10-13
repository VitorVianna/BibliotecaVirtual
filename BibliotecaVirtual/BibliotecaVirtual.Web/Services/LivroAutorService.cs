using BibliotecaVirtual.Web.Dados.Models;
using BibliotecaVirtual.Web.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotecaVirtual.Web.Services
{
    public class LivroAutorService
    {
        public static void VincularLivroAutor(LivroModel livro, AutorModel autor)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var vinculoExiste = db.LivroAutor
                                        .Include("Autor")
                                        .Include("Livro")
                                        .FirstOrDefault(l => l.Livro.Id == livro.Id && l.Autor.Id == autor.Id);

                    if (vinculoExiste == null)
                    {
                        string sql = String.Format("insert into LivroAutorModel values ({0},{1})", autor.Id, livro.Id);
                        db.Database.ExecuteSqlCommand(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DesvincularLivrosAutores(int? idLivro, int? idAutor = null)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var vinculoExiste = db.LivroAutor
                                        .Include("Autor")
                                        .Include("Livro")
                                        .Where(l => l.Livro.Id == idLivro || l.Autor.Id == idAutor);

                    if (vinculoExiste != null)
                    {
                        db.LivroAutor.RemoveRange(vinculoExiste);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}