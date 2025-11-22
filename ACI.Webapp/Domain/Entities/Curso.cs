namespace ACI.Webapp.Domain.Entities;

public class Curso : BaseEntity
{
   public string Nome { get; set; }
   public ICollection<Matricula> Matriculas { get; set; } = [];
}