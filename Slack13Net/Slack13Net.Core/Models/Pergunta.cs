using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Slack13Net.Core.Models
{
    public class Pergunta
    {
        [Key]
        public int PerguntaId { get; set; }
        
        [Required]
        public DateTime DataCadastro { get; set; }
        
        [Required]
        [StringLength( 100, ErrorMessage = "O nome não pode conter mais que 100 caracteres." )]  
        public string Autor { get; set; }
        
        [Required]
        [StringLength( 100, ErrorMessage = "O título não pode conter mais que 100 caracteres." )]  
        public string Titulo { get; set; }
        
        [Required]
        public string Descricao { get; set; }

        public string Tags { get; set; }
        
        [Required]
        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<Resposta> Respostas { get; set; }

    }
}