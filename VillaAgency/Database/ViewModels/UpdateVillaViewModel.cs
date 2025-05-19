using System.ComponentModel.DataAnnotations;

namespace VillaAgency.Database.ViewModels;

public class UpdateVillaViewModel
{
    [MinLength(3)]
    public string Name { get; set; }
    public decimal Price { get; set; }
    [MinLength(5)]
    public string Description { get; set; }
    public IFormFile? File { get; set; }
}
