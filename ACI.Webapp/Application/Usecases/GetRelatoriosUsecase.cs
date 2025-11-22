using ACI.Webapp.Application.Models;
using ACI.Webapp.Application.Models.ViewModels;
using ACI.Webapp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ACI.Webapp.Application.Usecases;

public class GetRelatoriosUsecase
{
    private readonly AlunoRepository _alunoRepository;
    private readonly CursoRepository _cursoRepository;
    private readonly MatriculaRepository _matriculaRepository;
    
    public GetRelatoriosUsecase(
        AlunoRepository alunoRepository,
        CursoRepository cursoRepository,
        MatriculaRepository matriculaRepository)
    {
        _alunoRepository = alunoRepository;
        _cursoRepository = cursoRepository;
        _matriculaRepository = matriculaRepository;
    }
    
    public async Task<Result<RelatoriosViewModel>> ExecuteAsync()
    {
        try
        {
            var alunosCadastrados = _alunoRepository.Alunos.ToList();
            var alunosPorCurso = _matriculaRepository.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .GroupBy(m => m.Curso.Nome)
                .Select(g => new AlunoPorCurso
                {
                    CursoNome = g.Key,
                    AlunoCount = g.Count()
                })
                .ToList();

                var cursosComTotalAlunos = _cursoRepository.Cursos
                    .Select(c => new CursoComTotalDeAlunos
                    {
                        CursoNome = c.Nome,
                        TotalDeAlunos = _matriculaRepository.Matriculas.Count(m => m.CursoId == c.Id)
                    })
                    .ToList();

            var relatorios = new RelatoriosViewModel
            {
                TotalAlunos = alunosCadastrados.Count,
                AlunosPorCurso = alunosPorCurso,
                CursosComTotalDeAlunos = cursosComTotalAlunos
            }; 
            
            return Result<RelatoriosViewModel>.Success(relatorios);
        }
        catch (Exception ex)
        {
            return Result<RelatoriosViewModel>.Failure($"Erro ao obter relatórios: {ex.Message}");
        }
    }
}