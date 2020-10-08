using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotecaVirtual.Web.Dados.Models
{
    public class AssuntoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<LivroAssuntoModel> Livros { get; set; }
    }
}