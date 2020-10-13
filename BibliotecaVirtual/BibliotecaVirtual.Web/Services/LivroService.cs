using BibliotecaVirtual.Web.Dados.Models;
using BibliotecaVirtual.Web.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BibliotecaVirtual.Web.Services
{
    public class LivroService 
    {
        public static LivroModel CriarLivro(LivroModel livro)
        {
            try
            {
                using (var db = new DataContext())
                {
                    if (db.Livros.Any(l => l.Titulo.ToUpper().Equals(livro.Titulo.ToUpper()) && l.Edicao == livro.Edicao))
                        throw new Exception("O Livro já foi cadastrado.");

                    db.Livros.Add(livro);
                    db.SaveChanges();
                    return livro;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static LivroModel EditarLivro(LivroModel livroNovo)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var livro = db.Livros
                                .Include("Autores")
                                .Include("Autores.Autor")
                                .Include("Assuntos")
                                .Include("Assuntos.Assunto").FirstOrDefault(l => l.Id == livroNovo.Id);
                    if (livro != null)
                    {
                        LivroAutorService.DesvincularLivrosAutores(livro.Id);
                        LivroAssuntoService.DesvincularLivrosAssuntos(livro.Id);
                        livro.AnoPublicacao = livroNovo.AnoPublicacao;
                        livro.Edicao = livroNovo.Edicao;
                        livro.Editora = livroNovo.Editora;
                        livro.Titulo = livroNovo.Titulo;
                        livro.Valor = livroNovo.Valor;
                        db.SaveChanges();

                        foreach (var assunto in livroNovo.Assuntos) {
                            LivroAssuntoService.VincularLivroAssunto(livro, assunto.Assunto);
                        }
                        foreach (var autor in livroNovo.Autores)
                        {
                            LivroAutorService.VincularLivroAutor(livro, autor.Autor);
                        }
                        return livro;
                    }
                    else
                        throw new Exception("Livro Não Encontrado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro Ao Gravar Livro! \nVerifique As Informações Inseridas \n" + ex.Message);
            }
        }

        public static string ExcluirLivro(int idLivro)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var livroExcluido = db.Livros.FirstOrDefault(l => l.Id == idLivro);
                    if (livroExcluido != null)
                    {
                        LivroAutorService.DesvincularLivrosAutores(livroExcluido.Id);
                        LivroAssuntoService.DesvincularLivrosAssuntos(livroExcluido.Id);
                        db.Livros.Remove(livroExcluido);
                        db.SaveChanges();

                        return "Livro Excluído Com Sucesso!";
                    }
                    else
                        return "Livro Não Encontrado.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro Ao Excluir Livro! \nVerifique As Informações Inseridas \n" + ex.Message);
            }
        }

        public static List<LivroModel> FiltrarLivros(LivroModel filtro)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var lista = db.Livros
                                .Include("Autores")
                                .Include("Autores.Autor")
                                .Include("Assuntos")
                                .Include("Assuntos.Assunto")
                                .ToList();
                    if (filtro.Id != 0)
                        lista = lista.Where(l => l.Id == filtro.Id).ToList();

                    if (!String.IsNullOrEmpty(filtro.Titulo))
                        lista = lista.Where(l => l.Titulo.Contains(filtro.Titulo)).ToList();

                    if (filtro.Valor.HasValue)
                        lista = lista.Where(l => l.Valor == filtro.Valor).ToList();

                    if (!String.IsNullOrEmpty(filtro.AnoPublicacao))
                        lista = lista.Where(l => l.AnoPublicacao.Equals(filtro.AnoPublicacao)).ToList();

                    if (filtro.Edicao.HasValue)
                        lista = lista.Where(l => l.Edicao == l.Edicao).ToList();

                    if (!String.IsNullOrEmpty(filtro.Editora))
                        lista = lista.Where(l => l.Editora.Contains(filtro.Editora)).ToList();

                    return lista;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<LivroModel> ListarLivros()
        {
            try
            {
                using (var db = new DataContext())
                {
                    var lista = db.Livros
                                .Include("Autores")
                                .Include("Autores.Autor")
                                .Include("Assuntos")
                                .Include("Assuntos.Assunto")
                                .ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}