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
        return View();
    }
}
