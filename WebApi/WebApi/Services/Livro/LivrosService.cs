using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dto.livro;
using WebApi.Models;

namespace WebApi.Services.Livro
{
    public class LivrosService : ILivroInterface
    {
        private readonly AppDbContext _context;

        public LivrosService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int IdLivro)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<Models.LivroModel>();
            try
            {
                var livros = await _context.Livro.Include(a => a.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == IdLivro);
                if (livros == null) {
                    resposta.Mensagem = "Livro nao Encontrado";
                    return resposta;
                }

                resposta.Dados = livros;
                resposta.Mensagem = "Livro encontrado";

                return resposta;
            }
            catch (Exception ex) 
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List< LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livro = await _context.Livro
                    .Include(a => a.Autor )
                    .Where(livroBanco => livroBanco.Autor.Id == idAutor).ToListAsync();
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro Encontrado";
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livros Encontrados";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> CriacaLivro(LivroCriacaoDto livroCriacaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var autor = await _context.Autor
                    .FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.Autor.Id);

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro de autor encontrado";
                    return resposta;
                }

                var livro = new LivroModel()
                {
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = autor

                };

                _context.Add(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livro.Include(a => a.Autor).ToListAsync();

                return resposta;

            }
            catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

       

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livro = await _context.Livro.Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id); 

                var autor = await _context.Autor
                    .FirstOrDefaultAsync(autorBanco => autorBanco.Id ==  livroEdicaoDto.Autor.Id);

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro de autor encontrado";
                    return resposta;
                }
                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro de livro encontrado";
                    return resposta;
                }

                livro.Titulo = livroEdicaoDto.Titulo;
                livro.Autor = autor;

                _context.Update(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livro.ToListAsync();

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }

            
        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try
            {
                var livro = await _context.Livro.Include(a => a.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro de livro encontrado";
                    return resposta;
                }

                _context.Remove(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livro.ToListAsync();
                resposta.Mensagem = "Livros Removidos";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            } 
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<Models.LivroModel>>();

            try
            {
                var Livros = await _context.Livro.Include(a => a.Autor).ToListAsync();
                resposta.Dados = Livros;
                resposta.Mensagem = "Livros Encontrados";

                return resposta;

            }
            catch (Exception ex) 
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        
    }
}
