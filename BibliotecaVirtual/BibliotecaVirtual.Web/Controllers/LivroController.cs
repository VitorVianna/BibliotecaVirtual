using BibliotecaVirtual.Web.Dados.Models;
using BibliotecaVirtual.Web.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BibliotecaVirtual.Controllers
{
    public class LivroController : Controller
    {
        // GET: Livro
        public ActionResult Index()
        {
            return View(MontarObjetoRetorno(LivroService.ListarLivros()));
        }

        // GET: Livro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivroModel filtro = new LivroModel
            {
                Id = (int)id
            };
            var retorno = LivroService.FiltrarLivros(filtro).FirstOrDefault();
            if (retorno == null)
            {
                return HttpNotFound();
            }
            return View(MontarObjetoRetornoSimples(retorno));
        }

        // GET: Livro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Livro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Editora,Edicao,AnoPublicacao,Valor,ValorMin,ValorMax,Autores,Assuntos")] ObjetoRetorno livro)
        {
            if (ModelState.IsValid)
            {
                var NovoLivro = LivroService.CriarLivro(new LivroModel
                {
                    AnoPublicacao = livro.AnoPublicacao,
                    Edicao = livro.Edicao,
                    Editora = livro.Editora,
                    Titulo = livro.Titulo,
                    Valor = livro.Valor
                });

                if (!String.IsNullOrEmpty(livro.Autores))
                {
                    var listaAutores = livro.Autores.Split(',');

                    foreach (var autor in listaAutores)
                    {
                        var novoAutor = AutorService.criarAutor(autor);
                        LivroAutorService.VincularLivroAutor(NovoLivro, novoAutor);
                    }
                }

                if (!String.IsNullOrEmpty(livro.Assuntos))
                {
                    var listaAssuntos = livro.Assuntos.Split(',');

                    foreach (var assunto in listaAssuntos)
                    {
                        var novoAssunto = AssuntoService.criarAssunto(assunto);
                        LivroAssuntoService.VincularLivroAssunto(NovoLivro, novoAssunto);
                    }
                }

                return RedirectToAction("Index");
            }

            return View(livro);
        }

        // GET: Livro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivroModel filtro = new LivroModel
            {
                Id = (int)id
            };
            var retorno = LivroService.FiltrarLivros(filtro).FirstOrDefault();
            if (retorno == null)
            {
                return HttpNotFound();
            }
            return View(MontarObjetoRetornoSimples(retorno));
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Editora,Edicao,AnoPublicacao,Valor,ValorMin,ValorMax,Autores,Assuntos")] ObjetoRetorno livro)
        {
            if (ModelState.IsValid)
            {
                var livroAutorModel = new List<LivroAutorModel>();
                var livroAssuntoModel = new List<LivroAssuntoModel>();

                if (!String.IsNullOrEmpty(livro.Autores))
                {
                    var listaAutores = livro.Autores.Split(',');

                    foreach (var autor in listaAutores)
                    {
                        livroAutorModel.Add(new LivroAutorModel
                        {
                            Autor = AutorService.criarAutor(autor)
                        });
                    }
                }

                if (!String.IsNullOrEmpty(livro.Assuntos))
                {
                    var listaAssuntos = livro.Assuntos.Split(',');

                    foreach (var assunto in listaAssuntos)
                    {
                        livroAssuntoModel.Add(new LivroAssuntoModel
                        {
                            Assunto = AssuntoService.criarAssunto(assunto)
                        });
                    }
                }

                LivroService.EditarLivro(new LivroModel
                {
                    AnoPublicacao = livro.AnoPublicacao,
                    Edicao = livro.Edicao,
                    Editora = livro.Editora,
                    Id = livro.Id,
                    Titulo = livro.Titulo,
                    Valor = livro.Valor,
                    Autores = livroAutorModel,
                    Assuntos = livroAssuntoModel
                });

                return RedirectToAction("Index");
            }
            return View(livro);
        }

        // GET: Livro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivroModel filtro = new LivroModel
            {
                Id = (int)id
            };
            var retorno = LivroService.FiltrarLivros(filtro).FirstOrDefault();
            if (retorno == null)
            {
                return HttpNotFound();
            }
            return View(MontarObjetoRetornoSimples(retorno));
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LivroService.ExcluirLivro(id);
            return RedirectToAction("Index");
        }

        public class ObjetoRetorno
        {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Editora { get; set; }
            public int? Edicao { get; set; }
            public string AnoPublicacao { get; set; }
            public Decimal Valor { get; set; }
            public decimal? ValorMin { get; set; }
            public decimal? ValorMax { get; set; }
            public string Autores { get; set; }
            public string Assuntos { get; set; }
        }

        private List<ObjetoRetorno> MontarObjetoRetorno(List<LivroModel> livro)
        {
            var retorno = new List<ObjetoRetorno>();
            retorno.AddRange(livro.Select(l => new ObjetoRetorno
            {
                AnoPublicacao = l.AnoPublicacao,
                Autores = String.Join(", ", l.Autores.Select(a => a.Autor.Nome)),
                Edicao = l.Edicao,
                Editora = l.Editora,
                Id = l.Id,
                Titulo = l.Titulo,
                Valor = (decimal)l.Valor,
                Assuntos = String.Join(", ", l.Assuntos.Select(a => a.Assunto.Descricao)),
            }));

            return retorno;
        }

        private ObjetoRetorno MontarObjetoRetornoSimples(LivroModel livro)
        {
            

            return new ObjetoRetorno
            {
                AnoPublicacao = livro.AnoPublicacao,
                Autores = livro.Autores == null ? "" : String.Join(", ", livro.Autores.Select(a => a.Autor.Nome)),
                Edicao = livro.Edicao,
                Editora = livro.Editora,
                Id = livro.Id,
                Titulo = livro.Titulo,
                Valor = (decimal)livro.Valor,
                Assuntos = livro.Assuntos == null ? "" : String.Join(", ", livro.Assuntos.Select(a => a.Assunto.Descricao)),
            };
        }
    }
}
