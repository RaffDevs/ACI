namespace ACI.Webapp.Domain.Entities;

public class Matricula : BaseEntity
{
   public int AlunoId { get; set; }
   public int CursoId { get; set; }
   public Aluno Aluno { get; set; }
   public Curso Curso { get; set; }
}