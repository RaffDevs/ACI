using System;
using ACI.Webapp.Domain.Entities;
using ACI.Webapp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ACI.Webapp.Infrastructure.Repositories;

public class AlunoRepository
{
    private readonly ApplicationDatabaseContext _context;

    public AlunoRepository(ApplicationDatabaseContext context)
    {
        _context = context;
    }

    public DbSet<Aluno> Alunos => _context.Alunos;
    
    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    internal async Task DeleteAsync(Aluno aluno)
    {
        throw new NotImplementedException();
    }
}
