using FilmesAPIServer.Exceptions;
using FilmesAPIServer.InputModel;
using FilmesAPIServer.Services;
using FilmesAPIServer.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPIServer.Controllers.v2
{
    [ApiController]
    [Route("v2")]
    public class FilmeController : Controller
    {
        private readonly IFilmeService _filmeService;

        public FilmeController(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpGet("filmes")]
        public async Task<ActionResult<FilmeViewModel>> Obter()
        {
            var filmes = await _filmeService.Obter();

            if (filmes.Count() == 0 )
                return NoContent();

            return Ok(filmes);
        }

        [HttpGet("filmes/{id}")]
        public async Task<ActionResult<FilmeViewModel>> Obter([FromRoute] int id)
        {
            var filme = await _filmeService.Obter(id);

            if(filme == null)
                return NoContent();

            return Ok(filme);
        }

        [HttpGet("filmes/{ano}")]
        public async Task<ActionResult<FilmeViewModel>> ObterAno([FromRoute] int ano)
        {
            var filmes = await _filmeService.ObterAno(ano);

            if(filmes.Count() == 0)
                return NoContent();

            return Ok(filmes);
        }

        [HttpPost("filmes")]
        public async Task<ActionResult<FilmeViewModel>> Inserir([FromBody] FilmeInputModel filmeInput)
        {
            try
            {
                var filme = await _filmeService.Inserir(filmeInput);

                return Ok(filme);
            }
            catch(FilmeJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um Filme com esse nome cadastrado");
            }
        }

        [HttpPut("filmes/{id}")]
        public async Task<ActionResult> Atualizar([FromRoute] int id, [FromBody] FilmeInputModel filmeInput)
        {
            try
            {
                await _filmeService.Atualizar(id, filmeInput);

                return Ok();
            }
            catch (FilmeNaoCadastradoException ex)
            {
                return NotFound("Filme não encontrado");
            }
        }

        [HttpDelete("filmes/{id}")]
        public async Task<ActionResult> Deletar([FromRoute] int id)
        {
            try
            {
                await _filmeService.Remover(id);

                return Ok();
            }
            catch (FilmeNaoCadastradoException ex)
            {
                return NotFound("Filme não encontrado");
            }
        }

    }
}
