using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLRAW.Context;
using SQLRAW.Models;

namespace SQLRAW.Controllers;
public class AlunosController : Controller
{
    private readonly AppDbContext _appDbContext;

    public AlunosController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IActionResult> Index()
    {
        var alunos = _appDbContext.Alunos.FromSql<Aluno>($@"SELECT
                                          Alunos.AlunoId,
                                          Alunos.Nome,
                                          Alunos.Cidade,
                                          Alunos.DataMatricula,
                                          Alunos.Ativo,
                                          Alunos.CursoId
                                         FROM
                                          Alunos");

        var resultado = await alunos.ToListAsync();
        return View(resultado);
    }

    public async Task<IActionResult> AlunosCursos(string cidade)
    {
        var alunos = _appDbContext.Database.SqlQuery<AlunoCurso>($@"SELECT
                                                            Alunos.AlunoId,
                                                            Alunos.Nome AS NomeAluno,
                                                            Alunos.Cidade AS CidadeAluno,
                                                            Alunos.DataMatricula AS DataMatriculaAluno,
                                                            Alunos.Ativo AS AlunoAtivo,
                                                            Curso.Id AS CursoId,
                                                            Curso.Nome AS NomeCurso,
                                                            Curso.Inicio AS InicioCurso
                                                        FROM
                                                            Alunos
                                                        INNER JOIN
                                                            Curso ON Alunos.CursoId = Curso.Id
                                                        WHERE (Alunos.Cidade = {cidade} OR {cidade} = '') 
                                                                                                   OR ({cidade} IS NULL)
                                                        ORDER BY Alunos.Nome");

        var resultado = await alunos.ToListAsync();

        return View(resultado);
    }
}
