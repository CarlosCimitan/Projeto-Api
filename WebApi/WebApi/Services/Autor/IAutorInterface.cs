
using WebApi.Dto.Autor;
using WebApi.Models;

namespace WebApi.Services.Autor
{
    public interface IAutorInterface
    {
        Task<ResponseModel<List<AutorModel>>> ListarAutores();
        Task<ResponseModel<AutorModel>> BuscarAutorPorId(int IdAutor);
        Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int IdLivro);
        Task<ResponseModel<List<AutorModel>>> CriacaoAutor(AutorCriacaoDto autorCriacaoDto);

        Task<ResponseModel<List<AutorModel>>> EditarAutor( AutorEdicaoDto autorEdicaoDto );
        Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor);
    }
}
