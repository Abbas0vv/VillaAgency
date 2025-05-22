using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VillaAgency.Database.Interfaces;
using VillaAgency.Database.ViewModels;
namespace VillaAgency.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly IVillaRepository _villaRepository;

    public DashboardController(IVillaRepository villaRepository)
    {
        _villaRepository = villaRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var villas = _villaRepository.GetAll();
        return View(villas);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(CreateVillaViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        _villaRepository.Insert(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Update(int? id)
    {
        if (id is null || _villaRepository.GetById(id) is null) return RedirectToAction(nameof(PageNotFound));
        var model = _villaRepository.GetByIdViewModel(id);
        return View(model);
    }

    [HttpPost]
    public IActionResult Update(int? id, UpdateVillaViewModel model)
    {
        if (id is null || _villaRepository.GetById(id) is null) return RedirectToAction(nameof(PageNotFound));
        if (!ModelState.IsValid) return View(model);
        _villaRepository.Update(id, model);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id is null || _villaRepository.GetById(id) is null) return RedirectToAction(nameof(PageNotFound));
        _villaRepository.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult PageNotFound()
    {
        return View();
    }
}
