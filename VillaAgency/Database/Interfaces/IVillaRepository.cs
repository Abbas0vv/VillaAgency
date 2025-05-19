using VillaAgency.Database.Models;
using VillaAgency.Database.ViewModels;

namespace VillaAgency.Database.Interfaces;

public interface IVillaRepository
{
    public List<Villa> GetAll();
    public List<Villa> GetSome(int value);
    public Villa GetById(int id);
    public UpdateVillaViewModel GetByIdViewModel(int id);
    public void Insert(CreateVillaViewModel model);
    public void Update(int id, UpdateVillaViewModel model);
    public void Delete(int id);
}
