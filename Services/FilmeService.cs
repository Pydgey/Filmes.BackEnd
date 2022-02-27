using FilmesAPIServer.Exceptions;
using FilmesAPIServer.InputModel;
using FilmesAPIServer.Models;
using FilmesAPIServer.Repositories;
using FilmesAPIServer.ViewModel;

namespace FilmesAPIServer.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly IFilmeRepository _filmeRepository;

        public FilmeService(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<List<FilmeViewModel>>Obter()
        {
            var filmes = await _filmeRepository.Obter();

            return filmes.Select(filme => new FilmeViewModel
            {
                Id = filme.Id,
                Titulo = filme.Title,
                Desc = filme.Desc,
                Ano = filme.Ano,
                CapaUrl = filme.CapaUrl
            }).ToList();
        }

        public async Task<FilmeViewModel> Obter(int id)
        {
            var filme = await _filmeRepository.Obter(id);

            if (filme == null)
                return null;

            return new FilmeViewModel
            {
                Id = filme.Id,
                Titulo = filme.Title,
                Desc = filme.Desc,
                Ano = filme.Ano,
                CapaUrl = filme.CapaUrl
            };
        }

        public async Task<List<FilmeViewModel>> ObterAno(int ano)
        {
            var filmes = await _filmeRepository.ObterAno(ano);

            return filmes.Select(filme => new FilmeViewModel
            {
                Id = filme.Id,
                Titulo = filme.Title,
                Desc = filme.Desc,
                Ano = filme.Ano,
                CapaUrl = filme.CapaUrl
            }).ToList();

        }

        public async Task<FilmeViewModel> Inserir(FilmeInputModel filme)
        {
            var queryFilme = await _filmeRepository.Obter(filme.Title);

            if (queryFilme.Count > 0)
                throw new FilmeJaCadastradoException();

            var filmeInsert = new Filmes
            {
                Title = filme.Title,
                Desc = filme.Desc,
                Ano = filme.Ano,
                CapaUrl = filme.CapaUrl
            };

            await _filmeRepository.Inserir(filmeInsert);

            return new FilmeViewModel
            {
                Id = filmeInsert.Id,
                Titulo = filmeInsert.Title,
                Desc = filmeInsert.Desc,
                Ano = filmeInsert.Ano,
                CapaUrl = filmeInsert.CapaUrl
            };
        }

        public async Task Atualizar(int id, FilmeInputModel filme)
        {
            var queryFilme = await _filmeRepository.Obter(id);

            if (queryFilme == null)
                throw new FilmeNaoCadastradoException();

            queryFilme.Title = filme.Title;
            queryFilme.Desc = filme.Desc;
            queryFilme.Ano = filme.Ano;
            queryFilme.CapaUrl = filme.CapaUrl;

            await _filmeRepository.Atualizar(queryFilme);
        }

        public async Task Remover(int id)
        {
            var filme = _filmeRepository.Obter(id);
            
            if (filme == null)
                throw new FilmeNaoCadastradoException();

            await _filmeRepository.Remover(id);
        }

        public void Dispose()
        {
            _filmeRepository?.Dispose();
        }
    }
}
