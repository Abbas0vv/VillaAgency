﻿using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        await _userRepository.RegisterUser(model);

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

        await _userRepository.LoginUser(model);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        await _userRepository.LogOut();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> CreateRole()
    {
        await _userRepository.CreateRole();
        return RedirectToAction("Index", "Home");
    }
}
