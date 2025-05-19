using VillaAgency.Database.ViewModels;

namespace VillaAgency.Database.Interfaces;

public interface IUserRepository
{
    public async Task RegisterUser(RegisterViewModel model)
    {
        return;
    }
    public async Task<bool> LoginUser(LoginViewModel model)
    {
        return false;
    }
    public async Task LogOut()
    {
        return;
    }
}
