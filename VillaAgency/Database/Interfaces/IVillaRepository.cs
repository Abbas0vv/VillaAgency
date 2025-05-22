using VillaAgency.Database.Models;
using VillaAgency.Database.ViewModels;

namespace VillaAgency.Database.Interfaces;

public interface IVillaRepository
{
    List<Villa> GetAll();
    List<Villa> GetSome(int value);
    Villa GetById(int? id);
    UpdateVillaViewModel GetByIdViewModel(int? id);
    void Insert(CreateVillaViewModel model);
    void Update(int? id, UpdateVillaViewModel model);
    void Delete(int? id);
}
