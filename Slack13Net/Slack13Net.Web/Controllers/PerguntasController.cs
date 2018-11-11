using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slack13Net.Core.Contexts;
using Slack13Net.Web.ViewModels;
using System.Web;
using Slack13Net.Core.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Slack13Net.Web.Controllers
{
    public class PerguntasController : Controller
    {
        private readonly Context _context;

        public PerguntasController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            try
            {
                var model = new PerguntasViewModel()
                {
                    Categoria = _context.Categorias.Find(id),
                    Perguntas = _context.Perguntas
                    .Where(p => p.CategoriaId == id)
                    .Include("Respostas")
                    .OrderBy(p => p.Descricao).ToList()
                };

                return View(model);

            }
            catch (Exception e)
            {
                throw e;
            }
        }        
        [HttpGet]
        public IEnumerable<Pergunta> GetPerguntas()
        {
            return _context.Perguntas;
        }

        [HttpPost]
        public IActionResult CadastrarPergunta(Pergunta pergunta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Perguntas.Add(pergunta);
            _context.SaveChanges();

            return RedirectToAction("Index", new { id = pergunta.CategoriaId });
        }

        [HttpGet]
        public IActionResult EditarPergunta(int id)
        {
            try
            {                
                Pergunta pergunta  = _context.Perguntas.Find(id);

                if (pergunta == null)
                    pergunta = new Pergunta();

                return View(pergunta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost]
        public IActionResult SalvarEditarPergunta(Pergunta pergunta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            _context.Entry(pergunta).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerguntaExists(pergunta.PerguntaId))
                {
                    return RedirectToAction("/Home/Index");
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index", new { id = pergunta.CategoriaId });
        }           
                    
        public IActionResult DeletarPergunta(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pergunta =  _context.Perguntas.Find(id);
            if (pergunta == null)
            {
                return NotFound();
            }

            _context.Perguntas.Remove(pergunta);
             _context.SaveChanges();

            return RedirectToAction("Index", new { id = pergunta.CategoriaId });
        }

        private bool PerguntaExists(int id)
        {
            return _context.Perguntas.Any(e => e.PerguntaId == id);
        }
    }
}