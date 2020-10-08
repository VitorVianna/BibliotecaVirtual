using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotecaVirtual.Web.Dados.Models
{
    public class LivroAssuntoModel
    {
        public int Id { get; set; }
        public virtual LivroModel Livro { get; set; }
        public virtual AssuntoModel Assunto { get; set; }
    }
}