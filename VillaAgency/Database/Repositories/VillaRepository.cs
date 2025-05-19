using Microsoft.AspNetCore.Mvc.ModelBinding;
using VillaAgency.Database.Interfaces;
using VillaAgency.Database.Models;
using VillaAgency.Database.ViewModels;
using VillaAgency.Helpers.Extentions;

namespace VillaAgency.Database.Repositories;

public class VillaRepository : IVillaRepository
{
    public readonly AppDbContext _context;
    public readonly IWebHostEnvironment _environment;
    public const string FOLDER_NAME = "Uploads/Villa";

    public VillaRepository(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public List<Villa> GetAll()
    {
        return _context.Villas.OrderBy(v => v.Id).ToList();
    }
    public List<Villa> GetSome(int value)
    {
        if (value >= GetAll().Count) return GetAll();
        return _context.Villas.OrderBy(v => v.Id).Take(value).ToList();
    }
    public Villa GetById(int? id)
    {
        return _context.Villas.FirstOrDefault(v => v.Id == id);
    }
    public void Insert(CreateVillaViewModel model)
    {
        var villa = new Villa()
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            ImageUrl = model.File.CreateFile(_environment.WebRootPath, FOLDER_NAME)
        };

        _context.Villas.Add(villa);
        _context.SaveChanges();
    }
    public UpdateVillaViewModel GetByIdViewModel(int? id)
    {
        var model = new UpdateVillaViewModel();
        if (id is null) return model;

        var villa = GetById(id);
        {
            model.Name = villa.Name;
            model.Description = villa.Description;
            model.Price = villa.Price;
        };

        return model;
    }
    public void Update(int? id, UpdateVillaViewModel model)
    {
        if (id is null) return;

        var villa = GetById(id);
        villa.Name = model.Name;
        villa.Description = model.Description;
        villa.Price = model.Price;
        if (model.File is not null)
            villa.ImageUrl = model.File.UpdateFile(_environment.WebRootPath, FOLDER_NAME, villa.ImageUrl);

        _context.Update(villa);
        _context.SaveChanges();
    }
    public void Delete(int? id)
    {
        var villa = GetById(id);
        FileExtention.RemoveFile(Path.Combine(_environment.WebRootPath, FOLDER_NAME, villa.ImageUrl));
        _context.Remove(villa);
        _context.SaveChanges();
    }

}
