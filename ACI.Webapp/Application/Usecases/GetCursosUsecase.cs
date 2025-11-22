using System;
using ACI.Webapp.Application.Models;
using ACI.Webapp.Domain.Entities;
using ACI.Webapp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ACI.Webapp.Application.Usecases;

public class GetCursosUsecase
{
    private readonly CursoRepository _cursoRepository;

    public GetCursosUsecase(CursoRepository cursoRepository)
    {
        _cursoRepository = cursoRepository;
    }
    
    public async Task<Result<List<Curso>>> ExecuteAsync()
    {
        try
        {
            var cursos = await _cursoRepository.Cursos
                .AsNoTracking()
                .ToListAsync();

            return Result<List<Curso>>.Success(cursos);
        }
        catch (Exception ex)
        {
            return Result<List<Curso>>.Failure("Erro ao obter cursos: " + ex.Message);
        }
    }
}
