using System;
using ACI.Webapp.Application.Models;
using ACI.Webapp.Application.Models.InputModels;
using ACI.Webapp.Domain.Entities;
using ACI.Webapp.Infrastructure.Repositories;

namespace ACI.Webapp.Application.Usecases;

public class CreateAlunoUsecase
{
    private readonly AlunoRepository _alunoRepository;

    public CreateAlunoUsecase(AlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<Result<Aluno>> ExecuteAsync(RegisterInputModel model)
    {
        try
        {
            var aluno = new Aluno
            {
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Email = model.Email,
                DataNascimento = model.DataNascimento
            };

            _alunoRepository.Alunos.Add(aluno);
            await _alunoRepository.SaveChangesAsync();
            return Result<Aluno>.Success(aluno);
        }
        catch (Exception ex)
        {
            return Result<Aluno>.Failure("Failed to create Aluno: " + ex.Message);
        }
    }
}
