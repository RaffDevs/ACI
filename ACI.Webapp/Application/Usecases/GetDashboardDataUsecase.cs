using System;
using ACI.Webapp.Application.Models;
using ACI.Webapp.Application.Models.ViewModels;
using ACI.Webapp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ACI.Webapp.Application.Usecases;

public class GetDashboardDataUsecase
{
    private readonly AlunoRepository _alunoRepository;
    private readonly CursoRepository _cursoRepository;
    private readonly MatriculaRepository _matriculaRepository;

    public GetDashboardDataUsecase(AlunoRepository alunoRepository, CursoRepository cursoRepository, MatriculaRepository matriculaRepository)
    {
        _alunoRepository = alunoRepository;
        _cursoRepository = cursoRepository;
        _matriculaRepository = matriculaRepository;
    }
    
    public async Task<Result<DashboardViewModel>> ExecuteAsync()
    {
        try
        {
            var alunos = await _alunoRepository.Alunos.ToListAsync();
            var cursos = await _cursoRepository.Cursos.ToListAsync();
            var matriculas = await _matriculaRepository.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .ToListAsync();

            var dashboardData = new DashboardViewModel
            {
                Alunos = alunos,
                Cursos = cursos,
                Matriculas = matriculas
            };

            return Result<DashboardViewModel>.Success(dashboardData);
        }
        catch (Exception ex)
        {
            return Result<DashboardViewModel>.Failure($"Erro ao obter dados do dashboard: {ex.Message}");
        }
    }

}
