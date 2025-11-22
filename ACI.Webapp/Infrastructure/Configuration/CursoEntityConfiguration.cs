using System;
using ACI.Webapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ACI.Webapp.Infrastructure.Configuration;

public class CursoEntityConfiguration : IEntityTypeConfiguration<Curso>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Curso> builder)
    {
        builder.HasKey(c => c.Id);
    }
}
