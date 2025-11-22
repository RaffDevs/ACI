using System;
using ACI.Webapp.Domain.Entities;

namespace ACI.Webapp.Application.Models.ViewModels;

public class DashboardViewModel
{
    public List<Aluno> Alunos { get; set; } = [];
    public List<Curso> Cursos { get; set; } = [];
    public List<Matricula> Matriculas { get; set; } = [];
}
