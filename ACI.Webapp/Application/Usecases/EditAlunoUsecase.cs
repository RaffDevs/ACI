using System;
using ACI.Webapp.Application.Models;
using ACI.Webapp.Application.Models.InputModels;
using ACI.Webapp.Domain.Entities;
using ACI.Webapp.Infrastructure.Repositories;

namespace ACI.Webapp.Application.Usecases;

public class EditAlunoUsecase
{
    private readonly AlunoRepository _alunoRepository;

    public EditAlunoUsecase(AlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<Result<bool>> ExecuteAsync(EditAlunoInputModel model)
    {
        try
        {
            var existingAluno = await _alunoRepository.Alunos.FindAsync(model.Id);
            if (existingAluno == null)
            {
                return Result<bool>.Failure("Aluno não encontrado.");
            }

            existingAluno.Nome = model.Nome;
            existingAluno.Sobrenome = model.Sobrenome;
            existingAluno.Email = model.Email;
            existingAluno.DataNascimento = model.DataNascimento;

            await _alunoRepository.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure("Erro ao editar aluno: " + ex.Message);
        }
    }
}
