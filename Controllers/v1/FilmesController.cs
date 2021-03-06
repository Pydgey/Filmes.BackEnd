using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using FilmesAPIServer.Data;
using System;
using FilmesAPIServer.Models;
using FilmesAPIServer.Exceptions;
using FilmesAPIServer.ViewModel;
using FilmesAPIServer.InputModel;

namespace FilmesAPIServer.Controllers
{
    [ApiController]
    [Route("v1")]
    public class FilmesController : ControllerBase
    {
        private readonly DataContext _context;
        public FilmesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("filmes")]
        public async Task<IActionResult> GetAsync()
        {
            var filme = await _context
                .filmes
                .AsNoTracking()
                .ToListAsync();

            if(filme.Count() == 0)
            {
                return NoContent();
            }
            return Ok(filme);
        }

        [HttpGet("filmes/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
        {
            var filme = await _context
                .filmes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return filme == null ? NotFound() : Ok(filme);
        }

        [HttpPost("filmes")]
        public async Task<ActionResult<Filmes>> PostAsync ([FromBody]Filmes filme)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            try 
            {
                await _context.filmes.AddAsync(filme);
                await _context.SaveChangesAsync();
                return Created($"v1/filmes/{filme.Id}", filme);
            }
            catch (FilmeJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um filme com esse nome cadastrado");
            }
        }

        [HttpPut("filmes/{id}")]
        public async Task<IActionResult> PutAsync([FromRoute]int id,[FromBody]FilmeInputModel filme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var model = await _context
                .filmes
                .FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
                return NotFound();

            try
            {
                model.Title = filme.Title ;
                model.Desc = filme.Desc ;
                model.Ano = filme.Ano;
                model.CapaUrl = filme.CapaUrl ;

                _context.filmes.Update(model);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch(FilmeNaoCadastradoException ex)
            {
                return NotFound("O Filme não foi encontrado");
            }
        }

        [HttpDelete("filmes/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            var model = await _context
                .filmes
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                _context.filmes.Remove(model);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (FilmeNaoCadastradoException ex)
            {
                return NotFound("O Filme não foi encontrado");
            }
        }
    }
}
