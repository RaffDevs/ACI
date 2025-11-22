using System;
using ACI.Webapp.Application.Models;
using ACI.Webapp.Domain.Entities;
using ACI.Webapp.Infrastructure.Repositories;

namespace ACI.Webapp.Application.Usecases;

public class CreateCursoUsecase
{
    private readonly CursoRepository _cursoRepository;

    public CreateCursoUsecase(CursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }

    public async Task<Result<bool>> ExecuteAsync(string nome)
    {
        try
        {
            var newCurso = new Curso
            {
                Nome = nome
            };

            await _cursoRepository.Cursos.AddAsync(newCurso);
            await _cursoRepository.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure("Erro ao criar curso: " + ex.Message);
        }
    }
}
