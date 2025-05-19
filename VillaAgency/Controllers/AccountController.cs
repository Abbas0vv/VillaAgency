using Microsoft.AspNetCore.Mvc;
using VillaAgency.Database.Interfaces;
using VillaAgency.Database.ViewModels;
namespace VillaAgency.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        _userRepository.RegisterUser(model);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var isSucceeded = await _userRepository.LoginUser(model);

        if (!isSucceeded) return View(model);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        await _userRepository.LogOut();
        return RedirectToAction("Index", "Home");
    }
}
