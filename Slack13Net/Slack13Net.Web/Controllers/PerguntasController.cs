using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slack13Net.Core.Contexts;

namespace Slack13Net.Web.Controllers
{
    public class PerguntasController : Controller
    {
        private readonly Context _context;

        public PerguntasController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            return View(_context.Perguntas
                .Where(p => p.CategoriaId == id)
                .Include("Respostas")
                .OrderBy(p => p.Descricao));
        }
    }
}