using System;
using ACI.Webapp.Domain.Entities;

namespace ACI.Webapp.Application.Models.ViewModels;

public class RelatoriosViewModel
{
    public int TotalAlunos { get; set; } = 0;
    public List<AlunoPorCurso> AlunosPorCurso { get; set; } = [];
    public List<CursoComTotalDeAlunos> CursosComTotalDeAlunos { get; set; } = [];

}
public class AlunoPorCurso
{
    public string CursoNome { get; set; }
    public int AlunoCount { get; set; }
}
public class CursoComTotalDeAlunos
{
    public string CursoNome { get; set; }
    public int TotalDeAlunos { get; set; }
}
