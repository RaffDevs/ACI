using System;
using ACI.Webapp.Application.Models;
using ACI.Webapp.Infrastructure.Repositories;

namespace ACI.Webapp.Application.Usecases;

public class EditCursoUsecase
{
    private readonly CursoRepository _cursoRepository;

    public EditCursoUsecase(CursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }

    public async Task<Result<bool>> ExecuteAsync(int id, string nome)
    {
        try
        {
            var existingCurso = await _cursoRepository.Cursos.FindAsync(id);
            if (existingCurso == null)
            {
                return Result<bool>.Failure("Curso não encontrado.");
            }

            existingCurso.Nome = nome;

            await _cursoRepository.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure("Erro ao editar curso: " + ex.Message);
        }
    }
}
