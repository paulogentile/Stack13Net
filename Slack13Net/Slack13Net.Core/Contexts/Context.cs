using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Slack13Net.Core.Models;

namespace Slack13Net.Core.Contexts
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) :
            base(options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pergunta> Perguntas { get; set; }
        public DbSet<Resposta> Respostas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var pb in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string))
                .Select(p => modelBuilder.Entity(p.DeclaringEntityType.ClrType).Property(p.Name)))
            {
                pb.IsUnicode(false);
            }

            
            // Relacionamentos
            modelBuilder.Entity<Categoria>()
                .HasMany(p => p.Perguntas)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId)
                .IsRequired();

            modelBuilder.Entity<Pergunta>()
                .HasMany(p => p.Respostas)
                .WithOne(p => p.Pergunta)
                .HasForeignKey(p => p.PerguntaId)
                .IsRequired();

            modelBuilder.Entity<Categoria>().HasData(new Categoria {CategoriaId = 1, Titulo = "JavaScript", Descricao = "Encontre aqui tudo que precisa saber sobre javascript"});
            modelBuilder.Entity<Categoria>().HasData(new Categoria {CategoriaId = 2, Titulo = "C#", Descricao = "Encontre aqui tudo que precisa saber sobre C#"});
            modelBuilder.Entity<Categoria>().HasData(new Categoria {CategoriaId = 3, Titulo = "jQuery", Descricao = "Encontre aqui tudo que precisa saber sobre jQuery"});
            modelBuilder.Entity<Categoria>().HasData(new Categoria {CategoriaId = 4, Titulo = "HTML", Descricao = "Encontre aqui tudo que precisa saber sobre HTML"});
            modelBuilder.Entity<Categoria>().HasData(new Categoria {CategoriaId = 5, Titulo = "SQL Server", Descricao = "Encontre aqui tudo que precisa saber sobre SQL Server"});
            modelBuilder.Entity<Categoria>().HasData(new Categoria {CategoriaId = 6, Titulo = "Asp.Net", Descricao = "Encontre aqui tudo que precisa saber sobre Asp.Net"});
            modelBuilder.Entity<Categoria>().HasData(new Categoria {CategoriaId = 7, Titulo = "Asp.Net Core", Descricao = "Encontre aqui tudo que precisa saber sobre Asp.Net Core"});
            modelBuilder.Entity<Categoria>().HasData(new Categoria {CategoriaId = 8, Titulo = "Json", Descricao = "Encontre aqui tudo que precisa saber sobre Json"});

            modelBuilder.Entity<Pergunta>().HasData(new Pergunta
                {
                    DataCadastro = DateTime.Now,
                    PerguntaId = 1,
                    CategoriaId = 1,
                    Titulo = "Como retornar um objeto literal num arrow function?",
                    Descricao =
                        "Quanto tento retornar um objeto literal com Arrow Function, dá um erro: <br>var items = [1, 2, 3].map( i => {valor: i, data: new Date() })<br>Como contornar isso no Javascript?",
                    Autor = "Wallace Maxters",
                    Tags = "javascript,característica-linguagem"
                }
            );

            modelBuilder.Entity<Resposta>().HasData(new Resposta
            {
                RespostaId = 1,
                PerguntaId = 1,
                Autor = "Jones Araujo",
                Descricao = "Vixi cara boa pergunta!",
                DataCadastro = DateTime.Now
            });

            modelBuilder.Entity<Pergunta>().HasData(new Pergunta
                {
                    DataCadastro = DateTime.Now,
                    PerguntaId = 2,
                    CategoriaId = 2,
                    Titulo =
                        "É possível reescrever qualquer código que use ponteiros (C#) sem usar ponteiros em Node.js?",
                    Descricao =
                        "É possível reescrever qualquer código em C/C# que use ponteiros de um modo que faça a mesma coisa sem utilizá-los?<br>Meu medo são códigos mais complexos. Os simples eu acredito que não haja dificuldades de se reescrever.<br>Como poderia substituir os ponteiros usando o mesmo conceito em JavaScript?",
                    Autor = "Adrian Matheus Fernandez",
                    Tags = "javascript,c#,c++,node.js,ponteiro"
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}