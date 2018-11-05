using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Slack13Net.Core.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        
        [Required]
        public string Descricao { get; set; }

        public virtual ICollection<Pergunta> Perguntas { get; set; }
    }
}