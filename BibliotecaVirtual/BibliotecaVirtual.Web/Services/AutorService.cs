using BibliotecaVirtual.Web.Dados.Models;
using BibliotecaVirtual.Web.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotecaVirtual.Web.Services
{
    public class AutorService
    {
        public static AutorModel criarAutor(string autor)
        {
            try
            {
                using (var db = new DataContext()) 
                {
                    var autorExiste = db.Autores.FirstOrDefault(a => a.Nome.ToUpper().Equals(autor.ToUpper()));
                    if (autorExiste != null)
                        return autorExiste;

                    var NovoAutor = new AutorModel
                    {
                        Nome = autor
                    };
                    db.Autores.Add(NovoAutor);
                    db.SaveChanges();
                    return NovoAutor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}