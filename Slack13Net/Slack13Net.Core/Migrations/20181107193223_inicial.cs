using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Slack13Net.Core.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(unicode: false, nullable: false),
                    Descricao = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Perguntas",
                columns: table => new
                {
                    PerguntaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Autor = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Titulo = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(unicode: false, nullable: false),
                    Tags = table.Column<string>(unicode: false, nullable: true),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perguntas", x => x.PerguntaId);
                    table.ForeignKey(
                        name: "FK_Perguntas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Respostas",
                columns: table => new
                {
                    RespostaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Autor = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(unicode: false, nullable: false),
                    PerguntaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respostas", x => x.RespostaId);
                    table.ForeignKey(
                        name: "FK_Respostas_Perguntas_PerguntaId",
                        column: x => x.PerguntaId,
                        principalTable: "Perguntas",
                        principalColumn: "PerguntaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "Descricao", "Titulo" },
                values: new object[,]
                {
                    { 1, "Encontre aqui tudo que precisa saber sobre javascript", "JavaScript" },
                    { 2, "Encontre aqui tudo que precisa saber sobre C#", "C#" },
                    { 3, "Encontre aqui tudo que precisa saber sobre jQuery", "jQuery" },
                    { 4, "Encontre aqui tudo que precisa saber sobre HTML", "HTML" },
                    { 5, "Encontre aqui tudo que precisa saber sobre SQL Server", "SQL Server" },
                    { 6, "Encontre aqui tudo que precisa saber sobre Asp.Net", "Asp.Net" },
                    { 7, "Encontre aqui tudo que precisa saber sobre Asp.Net Core", "Asp.Net Core" },
                    { 8, "Encontre aqui tudo que precisa saber sobre Json", "Json" }
                });

            migrationBuilder.InsertData(
                table: "Perguntas",
                columns: new[] { "PerguntaId", "Autor", "CategoriaId", "DataCadastro", "Descricao", "Tags", "Titulo" },
                values: new object[] { 1, "Wallace Maxters", 1, new DateTime(2018, 11, 7, 17, 32, 23, 40, DateTimeKind.Local), "Quanto tento retornar um objeto literal com Arrow Function, dá um erro: <br>var items = [1, 2, 3].map( i => {valor: i, data: new Date() })<br>Como contornar isso no Javascript?", "javascript,característica-linguagem", "Como retornar um objeto literal num arrow function?" });

            migrationBuilder.InsertData(
                table: "Perguntas",
                columns: new[] { "PerguntaId", "Autor", "CategoriaId", "DataCadastro", "Descricao", "Tags", "Titulo" },
                values: new object[] { 2, "Adrian Matheus Fernandez", 2, new DateTime(2018, 11, 7, 17, 32, 23, 46, DateTimeKind.Local), "É possível reescrever qualquer código em C/C# que use ponteiros de um modo que faça a mesma coisa sem utilizá-los?<br>Meu medo são códigos mais complexos. Os simples eu acredito que não haja dificuldades de se reescrever.<br>Como poderia substituir os ponteiros usando o mesmo conceito em JavaScript?", "javascript,c#,c++,node.js,ponteiro", "É possível reescrever qualquer código que use ponteiros (C#) sem usar ponteiros em Node.js?" });

            migrationBuilder.InsertData(
                table: "Respostas",
                columns: new[] { "RespostaId", "Autor", "DataCadastro", "Descricao", "PerguntaId" },
                values: new object[] { 1, "Jones Araujo", new DateTime(2018, 11, 7, 17, 32, 23, 46, DateTimeKind.Local), "Vixi cara boa pergunta!", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Perguntas_CategoriaId",
                table: "Perguntas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Respostas_PerguntaId",
                table: "Respostas",
                column: "PerguntaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Respostas");

            migrationBuilder.DropTable(
                name: "Perguntas");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
