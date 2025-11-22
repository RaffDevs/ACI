using System;
using ACI.Webapp.Application.Models;
using ACI.Webapp.Domain.Entities;
using ACI.Webapp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ACI.Webapp.Application.Usecases;

public class GetAlunoMatriculaUsecase
{
    private readonly AlunoRepository _alunoRepository;

    public GetAlunoMatriculaUsecase(AlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }
    
    public async Task<Result<Aluno>> ExecuteAsync(string email)
    {
        try
        {
            var result = await _alunoRepository.Alunos
                .Include(a => a.Matriculas)
                .ThenInclude(m => m.Curso)
                .FirstOrDefaultAsync(aluno => aluno.Email == email);

            if (result == null)
            {
                return Result<Aluno>.Failure("Aluno not found");
            }
              
            Console.WriteLine("Aluno found: " + result.Matriculas.Count);
            return Result<Aluno>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<Aluno>.Failure(ex.Message);
        }
    }
}
