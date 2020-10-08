using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotecaVirtual.Web.Dados.Models
{
    public class LivroAutorModel
    {
        public int Id { get; set; }
        public virtual LivroModel Livro { get; set; }
        public virtual AutorModel Autor { get; set; }
    }
}