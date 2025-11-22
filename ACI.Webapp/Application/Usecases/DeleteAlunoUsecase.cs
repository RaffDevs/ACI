using System;
using System.Reflection.Metadata.Ecma335;
using ACI.Webapp.Application.Models;
using ACI.Webapp.Domain.Entities;
using ACI.Webapp.Infrastructure.Repositories;

namespace ACI.Webapp.Application.Usecases;

public class DeleteAlunoUsecase
{
    private readonly AlunoRepository _alunoRepository;

    public DeleteAlunoUsecase(AlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<Result<Aluno>> ExecuteAsync(int alunoId)
    {
        try
        {
            var aluno = await _alunoRepository.Alunos.FindAsync(alunoId);
            if (aluno == null)
            {
                return Result<Aluno>.Failure("Aluno não encontrado.");
            }
            _alunoRepository.Alunos.Remove(aluno);
            await _alunoRepository.SaveChangesAsync();
            return Result<Aluno>.Success(aluno);
        }
        catch (Exception ex)
        {
            return Result<Aluno>.Failure($"Erro ao deletar aluno: {ex.Message}");
        }
    }
}
