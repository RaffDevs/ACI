using System.Reflection;
using ACI.Webapp.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ACI.Webapp.Infrastructure.Context;

public class ApplicationDatabaseContext : IdentityDbContext
{
   public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options) { }
   
   public DbSet<Aluno> Alunos { get; set; }
   public DbSet<Curso> Cursos { get; set; }
   public DbSet<Matricula> Matriculas { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
      base.OnModelCreating(builder);
    }
}