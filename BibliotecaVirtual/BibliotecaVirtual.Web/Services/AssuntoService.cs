using BibliotecaVirtual.Web.Dados;
using BibliotecaVirtual.Web.Dados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotecaVirtual.Web.Services
{
    public class AssuntoService
    {
        public static AssuntoModel criarAssunto(string assunto)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var assuntoExiste = db.Assuntos.FirstOrDefault(a => a.Descricao.ToUpper().Equals(assunto.ToUpper()));
                    if (assuntoExiste != null)
                        return assuntoExiste;

                    var NovoAssunto = new AssuntoModel
                    {
                        Descricao = assunto
                    };
                    db.Assuntos.Add(NovoAssunto);
                    db.SaveChanges();
                    return NovoAssunto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}