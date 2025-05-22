using VillaAgency.Database.ViewModels;

namespace VillaAgency.Database.Interfaces;

public interface IUserRepository
{
    Task RegisterUser(RegisterViewModel model);
    Task LoginUser(LoginViewModel model);
    Task LogOut();
    Task CreateRole();
}
