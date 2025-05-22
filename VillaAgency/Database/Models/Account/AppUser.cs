using Microsoft.AspNetCore.Identity;

namespace VillaAgency.Database.Models.Account;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
}
