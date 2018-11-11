using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slack13Net.Core.Contexts;
using Slack13Net.Core.Models;
using Slack13Net.Web.ViewModels;

namespace Slack13Net.Web.Controllers
{
    public class RespostasController : Controller
    {
        private readonly Context _context;

        public RespostasController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var model = new RespostasViewModel()
            {
                Pergunta = _context.Perguntas.Find(id),
                Respostas = _context.Respostas
                    .Where(p => p.PerguntaId == id)
                    .OrderBy(p => p.Descricao).ToList()
            };

            return View(model);
        }        
        [HttpPost]
        public IActionResult CadastrarResposta(Resposta resposta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Respostas.Add(resposta);
             _context.SaveChanges();

            return RedirectToAction("Index", new { id = resposta.PerguntaId});
        }

        [HttpGet]
        public IActionResult EditarResposta(int id)
        {
            try
            {
                Resposta resposta = _context.Respostas.Find(id);

                if (resposta == null)
                    resposta = new Resposta();

                return View(resposta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public IActionResult SalvarEditarResposta(Resposta resposta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(resposta).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!RespostaExists(resposta.RespostaId))
                {
                    return RedirectToAction("Index", new { id = resposta.PerguntaId });
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index", new { id = resposta.PerguntaId });
        }

        public IActionResult DeletarResposta(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resposta =  _context.Respostas.Find(id);
            if (resposta == null)
            {
                return NotFound();
            }

            _context.Respostas.Remove(resposta);
             _context.SaveChanges();

            return RedirectToAction("Index", new { id = resposta.PerguntaId });
        }

        private bool RespostaExists(int id)
        {
            return _context.Respostas.Any(e => e.RespostaId == id);
        }
    }
}