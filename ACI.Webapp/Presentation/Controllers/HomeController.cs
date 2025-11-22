using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ACI.Webapp.Models;
using Microsoft.AspNetCore.Authorization;
using ACI.Webapp.Application.Usecases;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ACI.Webapp.Presentation.Extensions;
using ACI.Webapp.Application.Enums;
using ACI.Webapp.Application.Models.InputModels;

namespace ACI.Webapp.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Admin([FromServices] GetDashboardDataUsecase usecase)
    {
        var result = await usecase.ExecuteAsync();

        if (result.IsSuccess)
        {
            return View(result.Data);
        }

        this.AddToastMessage(result.ErrorMessage, ToastType.Error, "Erro");
        return View();
    }

    [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<IActionResult> Aluno([FromServices] GetAlunoMatriculaUsecase usecase)
    {
        var user = await _userManager.GetUserAsync(User);
        var result = await usecase.ExecuteAsync(user.Email);

        if (result.IsSuccess)
        {
            return View(result.Data);
        }

        this.AddToastMessage(result.ErrorMessage, ToastType.Error, "Erro");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Matricula([FromServices] GetCursosUsecase usecase)
    {
        var result = await usecase.ExecuteAsync();

        if (result.IsSuccess)
        {
            return View(result.Data);
        }

        this.AddToastMessage(result.ErrorMessage, ToastType.Error, "Erro");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Matricula([FromServices] CreateMatriculaUsecase usecase, string cursoId)
    {
        var user = await _userManager.GetUserAsync(User);
        var result = await usecase.ExecuteAsync(cursoId, user.Email);

        if (result.IsSuccess)
        {
            this.AddToastMessage("Matrícula realizada com sucesso!", ToastType.Success, "Sucesso");
            return RedirectToAction("Aluno");
        }

        this.AddToastMessage(result.ErrorMessage, ToastType.Error, "Erro");
        return RedirectToAction("Matricula");
    }

    [HttpPost("/Aluno/Edit/{id}")]
    public async Task<IActionResult> EditAluno([FromServices] EditAlunoUsecase usecase, int id, string nome, string sobrenome, string email, DateTime dataNascimento)
    {
        var model = new EditAlunoInputModel
        {
            Id = id,
            Nome = nome,
            Sobrenome = sobrenome,
            Email = email,
            DataNascimento = dataNascimento
        };

        var result = await usecase.ExecuteAsync(model);
        if (!result.IsSuccess)
        {
            this.AddToastMessage(result.ErrorMessage, ToastType.Error, "Erro");
            return RedirectToAction("Admin");
        }
        else
        {
            this.AddToastMessage("Aluno editado com sucesso!", ToastType.Success, "Sucesso");
            return RedirectToAction("Admin");
        }
    }

    [HttpPost("/Aluno/Delete/{id}")]
    public async Task<IActionResult> DeleteAluno([FromServices] DeleteAlunoUsecase usecase, int id)
    {

        var result = await usecase.ExecuteAsync(id);
        if (!result.IsSuccess)
        {
            this.AddToastMessage(result.ErrorMessage, ToastType.Error, "Erro");
            return RedirectToAction("Admin");
        }
        else
        {
            this.AddToastMessage("Aluno deletado com sucesso!", ToastType.Success, "Sucesso");
            return RedirectToAction("Admin", "Home");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCurso([FromServices] CreateCursoUsecase usecase, string curso)
    {
        var result = await usecase.ExecuteAsync(curso);

        if (result.IsSuccess)
        {
            this.AddToastMessage("Curso criado com sucesso!", ToastType.Success, "Sucesso");
            return RedirectToAction("Admin");
        }

        this.AddToastMessage(result.ErrorMessage, ToastType.Error, "Erro");
        return RedirectToAction("Admin");
    }

    [HttpPost("/Curso/Edit/{id}")]
    public async Task<IActionResult> EditCurso([FromServices] EditCursoUsecase usecase, int id, string curso)
    {
        var result = await usecase.ExecuteAsync(id, curso);
        if (!result.IsSuccess)
        {
            this.AddToastMessage(result.ErrorMessage, ToastType.Error, "Erro");
            return RedirectToAction("Admin");
        }
        else
        {
            this.AddToastMessage("Aluno editado com sucesso!", ToastType.Success, "Sucesso");
            return RedirectToAction("Admin");
        }

    }

    [HttpPost("/Curso/Delete/{id}")]
    public async Task<IActionResult> DeleteCurso([FromServices] DeleteCursoUsecase usecase, int id)
    {
        var result = await usecase.ExecuteAsync(id);
        if (!result.IsSuccess)
        {
            this.AddToastMessage(result.ErrorMessage, ToastType.Error, "Erro");
            return RedirectToAction("Admin");
        }
        else
        {
            this.AddToastMessage("Curso deletado com sucesso!", ToastType.Success, "Sucesso");
            return RedirectToAction("Admin", "Home");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Relatorios([FromServices] GetRelatoriosUsecase usecase)
    {
        var result = await usecase.ExecuteAsync();

        if (result.IsSuccess)
        {
            return View(result.Data);
        }

        this.AddToastMessage(result.ErrorMessage, ToastType.Error, "Erro");
        return View();
    }
}