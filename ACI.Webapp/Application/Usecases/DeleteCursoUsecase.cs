using System;
using ACI.Webapp.Application.Models;
using ACI.Webapp.Domain.Entities;
using ACI.Webapp.Infrastructure.Repositories;

namespace ACI.Webapp.Application.Usecases;

public class DeleteCursoUsecase
{
    private readonly CursoRepository _cursoRepository;

    public DeleteCursoUsecase(CursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }

    public async Task<Result<Curso>> ExecuteAsync(int cursoId)
    {
        try
        {
            var curso = await _cursoRepository.Cursos.FindAsync(cursoId);
            if (curso == null)
            {
                return Result<Curso>.Failure("Curso não encontrado.");
            }
            _cursoRepository.Cursos.Remove(curso);
            await _cursoRepository.SaveChangesAsync();
            return Result<Curso>.Success(curso);
        }
        catch (Exception ex)
        {
            return Result<Curso>.Failure($"Erro ao deletar curso: {ex.Message}");
        }
    }
}
