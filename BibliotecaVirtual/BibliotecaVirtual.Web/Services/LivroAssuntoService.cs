using BibliotecaVirtual.Web.Dados;
using BibliotecaVirtual.Web.Dados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotecaVirtual.Web.Services
{
    public class LivroAssuntoService
    {
        public static void VincularLivroAssunto(LivroModel livro, AssuntoModel assunto)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var vinculoExiste = db.LivroAssunto
                                        .Include("Assunto")
                                        .Include("Livro")
                                        .FirstOrDefault(l => l.Livro.Id == livro.Id && l.Assunto.Id == assunto.Id);

                    if (vinculoExiste == null)
                    {
                        string sql = String.Format("insert into LivroAssuntoModel values ({0},{1})",assunto.Id, livro.Id);
                        db.Database.ExecuteSqlCommand(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DesvincularLivrosAssuntos(int? idLivro, int? idAssunto = null)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var vinculoExiste = db.LivroAssunto
                                        .Include("Assunto")
                                        .Include("Livro")
                                        .Where(l => l.Livro.Id == idLivro || l.Assunto.Id == idAssunto);

                    if (vinculoExiste != null)
                    {
                        db.LivroAssunto.RemoveRange(vinculoExiste);
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