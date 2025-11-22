using System;
using ACI.Webapp.Domain.Entities;
using ACI.Webapp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ACI.Webapp.Infrastructure.Repositories;

public class MatriculaRepository
{
    private readonly ApplicationDatabaseContext _context;

    public MatriculaRepository(ApplicationDatabaseContext context)
    {
        _context = context;
    }

    public DbSet<Matricula> Matriculas => _context.Matriculas;
    
    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
