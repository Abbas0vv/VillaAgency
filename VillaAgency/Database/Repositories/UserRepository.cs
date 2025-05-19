using Microsoft.AspNetCore.Identity;
using VillaAgency.Database.Interfaces;
using VillaAgency.Database.Models.Account;
using VillaAgency.Database.ViewModels;

namespace VillaAgency.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public UserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task RegisterUser(RegisterViewModel model)
    {

        var user = new AppUser()
        {
            Name = model.Name,
            Surname = model.Surname,
            Email = model.Email,
        };

        await _userManager.CreateAsync(user, model.Password);
    }

    public async Task<bool> LoginUser(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);

        return result.Succeeded;
    }

    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }
}
