using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Slack13Net.Core.Contexts;
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
    }
}