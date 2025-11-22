using System;

namespace ACI.Webapp.Application.Models.InputModels;

public class RegisterInputModel
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public DateTime DataNascimento { get; set; } 
    public string Email { get; set; }
    public string Password { get; set; }
}
