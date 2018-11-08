using System.Collections.Generic;
using Slack13Net.Core.Models;

namespace Slack13Net.Web.ViewModels
{
    public class PerguntasViewModel
    {
        public Categoria Categoria { get; set; }
        public List<Pergunta> Perguntas { get; set; }
    }
}
