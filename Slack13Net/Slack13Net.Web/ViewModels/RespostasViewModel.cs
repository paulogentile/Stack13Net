using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slack13Net.Core.Models;

namespace Slack13Net.Web.ViewModels
{
    public class RespostasViewModel
    {
        public Pergunta Pergunta { get; set; }
        public List<Resposta> Respostas { get; set; }
    }
}
