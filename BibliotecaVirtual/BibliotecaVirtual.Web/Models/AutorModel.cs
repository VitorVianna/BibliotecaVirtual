using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotecaVirtual.Web.Dados.Models
{
    public class AutorModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<LivroAutorModel> Livros { get; set; }
    }
}