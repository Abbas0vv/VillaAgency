using Microsoft.AspNetCore.Mvc;
using VillaAgency.Database.Interfaces;

namespace VillaAgency.Controllers;

public class HomeController : Controller
{
    private readonly IVillaRepository _repository;

    public HomeController(IVillaRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var villas = _repository.GetSome(3);
        return View(villas);
    }
}
