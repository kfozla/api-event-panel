using api_event_panel.Models;

namespace api_event_panel.Repositories;

public interface IVillaRepository
{
    public Task AddVilla(VillaModel villa);
    public Task<List<VillaModel>> GetAllVillas();
    public Task<VillaModel> GetVilla(int id);
    public Task<VillaModel> UpdateVilla(VillaModel villa);
    public Task DeleteVilla(int id);
}