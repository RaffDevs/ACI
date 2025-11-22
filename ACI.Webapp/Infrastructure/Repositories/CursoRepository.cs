using System;
using ACI.Webapp.Domain.Entities;
using ACI.Webapp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ACI.Webapp.Infrastructure.Repositories;

public class CursoRepository
{
    private readonly ApplicationDatabaseContext _context;

    public CursoRepository(ApplicationDatabaseContext context)
    {
        _context = context;
    }

    public DbSet<Curso> Cursos => _context.Cursos; 
    
    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
