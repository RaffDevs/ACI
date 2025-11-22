using System;
using ACI.Webapp.Application.Models;
using ACI.Webapp.Domain.Entities;
using ACI.Webapp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ACI.Webapp.Application.Usecases;

public class CreateMatriculaUsecase
{
    private readonly MatriculaRepository _matriculaRepository;
    private readonly AlunoRepository _alunoRepository;

    public CreateMatriculaUsecase(MatriculaRepository matriculaRepository, AlunoRepository alunoRepository)
    {
        _matriculaRepository = matriculaRepository;
        _alunoRepository = alunoRepository;
    }
    
    public async Task<Result<Matricula>> ExecuteAsync(string cursoId, string email)
    {
        try
        {
            var aluno = await _alunoRepository.Alunos
                .FirstOrDefaultAsync(a => a.Email == email);

            if (aluno == null)
            {
                return Result<Matricula>.Failure("Aluno não encontrado.");
            }

            var matricula = new Matricula
            {
                AlunoId = aluno.Id,
                CursoId = Convert.ToInt32(cursoId),
            };

            await _matriculaRepository.Matriculas.AddAsync(matricula);
            await _matriculaRepository.SaveChangesAsync();

            return Result<Matricula>.Success(matricula);
        }
        catch (Exception ex)
        {
            return Result<Matricula>.Failure($"Erro ao criar matrícula: {ex.Message}");
        }
    }

}
