using WebApi.Dto.Autor;
using WebApi.Dto.livro;
using WebApi.Models;

namespace WebApi.Services.Livro
{
    public interface ILivroInterface
    {
        Task<ResponseModel<List<LivroModel>>> ListarLivros();
        Task<ResponseModel<LivroModel>> BuscarLivroPorId(int IdLivro);
        Task<ResponseModel<List< LivroModel>>> BuscarLivroPorIdAutor(int IdAutor);
        
        Task<ResponseModel<List<LivroModel>>> CriacaLivro(LivroCriacaoDto livroCriacaoDto);

        Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro);
        Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroCriacaoDto);

    }
}
