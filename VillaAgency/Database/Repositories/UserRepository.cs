using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VillaAgency.Database.Interfaces;
using VillaAgency.Database.Models.Account;
using VillaAgency.Database.ViewModels;
using VillaAgency.Helpers.Enums;

namespace VillaAgency.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }
    public async Task RegisterUser(RegisterViewModel model)
    {
        var count = await _userManager.Users.CountAsync();
        var user = new AppUser()
        {
            Name = model.Name,
            Surname = model.Surname,
            UserName = model.Username,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            if (count == 0)
                await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            else
                await _userManager.AddToRoleAsync(user, Roles.User.ToString());

            await _signInManager.SignInAsync(user, true);
        }
    }

    public async Task LoginUser(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is not null)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
            if (result.Succeeded)
                await _signInManager.SignInAsync(user, true);
        }
    }

    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task CreateRole()
    {
        foreach (var item in Enum.GetValues(typeof(Roles)))
        {
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = item.ToString()
            });
        }
    }
}
