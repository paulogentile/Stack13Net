using System;
using System.ComponentModel.DataAnnotations;

namespace Slack13Net.Core.Models
{
    public class Resposta
    {
        [Key]
        public int RespostaId { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }
        
        [Required]
        [StringLength( 100, ErrorMessage = "O nome não pode conter mais que 100 caracteres." )]  
        public string Autor { get; set; }
        
        [Required]
        public string Descricao { get; set; }
        
        [Required]
        public int PerguntaId { get; set; }

        public virtual Pergunta Pergunta { get; set; }
    }
}