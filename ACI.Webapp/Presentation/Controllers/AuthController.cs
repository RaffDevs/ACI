using ACI.Webapp.Application.Enums;
using ACI.Webapp.Application.Models.InputModels;
using ACI.Webapp.Application.Usecases;
using ACI.Webapp.Presentation.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ACI.Webapp.Presentation.Controllers;

public class AuthController : Controller
{
   private readonly SignInManager<IdentityUser> _signInManager;
   private readonly UserManager<IdentityUser> _userManager;
   public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
   {
      _signInManager = signInManager;
      _userManager = userManager;
   }

   [HttpGet]
   public IActionResult Login()
   {
      return View();
   }

   [HttpPost]
   public async Task<IActionResult> Login(LoginInputModel model)
   {
      if (!ModelState.IsValid)
      {
         var errorMessages = string.Join(" ", ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage));

         TempData["ValidationErrors"] = errorMessages;
         this.AddToastMessage(errorMessages, ToastType.Error, "Erro");
         return View(model);
      }

      var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

      if (!result.Succeeded)
      {
         this.AddToastMessage("Credenciais inválidas. Tente novamente.", ToastType.Error, "Erro");
         return View(model);
      }

      var user = await _userManager.GetUserAsync(User);

      if (await _userManager.IsInRoleAsync(user, "Admin"))
      {
         return RedirectToAction("Admin", "Home");
      }
      
      return RedirectToAction("Aluno", "Home");
   }

   [HttpGet]
   public IActionResult Register()
   {
      return View();
   }

   [HttpPost]
   public async Task<IActionResult> Register([FromServices] CreateAlunoUsecase usecase, RegisterInputModel model)
   {
      if (!ModelState.IsValid)
      {
         var errorMessages = string.Join(" ", ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage));

         TempData["ValidationErrors"] = errorMessages;
         this.AddToastMessage(errorMessages, ToastType.Error, "Erro");
         return View(model);
      }

      var user = new IdentityUser { UserName = model.Email, Email = model.Email };
      var result = await _userManager.CreateAsync(user, model.Password);

      if (!result.Succeeded)
      {
         var errors = string.Join(" ", result.Errors.Select(e => e.Description));
         this.AddToastMessage(errors, ToastType.Error, "Erro");
         return View(model);
      }

      var alunoResult = await usecase.ExecuteAsync(model);

      if (alunoResult.IsSuccess)
      {
         await _userManager.AddToRoleAsync(user, "User");
         await _signInManager.SignInAsync(user, isPersistent: false);
         return RedirectToAction("Aluno", "Home");
      }

      this.AddToastMessage(alunoResult.ErrorMessage, ToastType.Error, "Erro");
      return View(model);
   }

   [HttpPost]
   public async Task<IActionResult> Logout()
   {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Login", "Auth");
   }
}