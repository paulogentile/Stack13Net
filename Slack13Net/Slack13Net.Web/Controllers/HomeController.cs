using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Slack13Net.Core.Contexts;
using Slack13Net.Web.Models;

namespace Slack13Net.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Categorias
                .Include("Perguntas")
                .OrderBy(p => p.Descricao));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
