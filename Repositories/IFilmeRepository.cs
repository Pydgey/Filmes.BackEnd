using FilmesAPIServer.InputModel;
using FilmesAPIServer.Models;
using FilmesAPIServer.ViewModel;

namespace FilmesAPIServer.Repositories
{
    public interface IFilmeRepository : IDisposable
    {
        Task<List<Filmes>> Obter();
        Task<Filmes> Obter(int id);
        Task<List<Filmes>> ObterAno(int ano);
        Task<List<Filmes>>Obter(string title);
        Task Inserir(Filmes filme);
        Task Atualizar(Filmes filme);
        Task Remover(int id);
    }
}
