using FilmesAPIServer.InputModel;
using FilmesAPIServer.ViewModel;

namespace FilmesAPIServer.Services
{
    public interface IFilmeService : IDisposable
    {
        Task<List<FilmeViewModel>> Obter();
        Task<FilmeViewModel> Obter(int id);
        Task<List<FilmeViewModel>> ObterAno(int ano);
        Task<FilmeViewModel> Inserir(FilmeInputModel filme);
        Task Atualizar(int id, FilmeInputModel filme);
        Task Remover(int id);
    }
}
