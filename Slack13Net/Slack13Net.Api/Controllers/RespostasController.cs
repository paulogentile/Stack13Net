using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slack13Net.Core.Contexts;
using Slack13Net.Core.Models;

namespace Slack13Net.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespostasController : ControllerBase
    {
        private readonly Context _context;

        public RespostasController(Context context)
        {
            _context = context;
        }

        // GET: api/Respostas
        [HttpGet]
        public IEnumerable<Resposta> GetRespostas()
        {
            return _context.Respostas;
        }

        // GET: api/Respostas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetResposta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resposta = await _context.Respostas.FindAsync(id);

            if (resposta == null)
            {
                return NotFound();
            }

            return Ok(resposta);
        }

        // PUT: api/Respostas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResposta([FromRoute] int id, [FromBody] Resposta resposta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resposta.RespostaId)
            {
                return BadRequest();
            }

            _context.Entry(resposta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RespostaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Respostas
        [HttpPost]
        public async Task<IActionResult> PostResposta([FromBody] Resposta resposta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Respostas.Add(resposta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResposta", new { id = resposta.RespostaId }, resposta);
        }

        // DELETE: api/Respostas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResposta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resposta = await _context.Respostas.FindAsync(id);
            if (resposta == null)
            {
                return NotFound();
            }

            _context.Respostas.Remove(resposta);
            await _context.SaveChangesAsync();

            return Ok(resposta);
        }

        private bool RespostaExists(int id)
        {
            return _context.Respostas.Any(e => e.RespostaId == id);
        }
    }
}