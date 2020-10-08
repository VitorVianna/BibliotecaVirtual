using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotecaVirtual.Web.Dados.Models
{
    public class LivroModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int? Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public decimal? Valor { get; set; }
        public decimal? ValorMin { get; set; }
        public decimal? ValorMax { get; set; }
        public virtual ICollection<LivroAutorModel> Autores { get; set; }
        public virtual ICollection<LivroAssuntoModel> Assuntos { get; set; }
    }
}