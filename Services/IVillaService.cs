using api_event_panel.Dtos;
using api_event_panel.Models;

namespace api_event_panel.Services;

public interface IVillaService
{
    Task<List<VillaModel>> GetAllVillas();
    Task<VillaModel> GetVillaById(int id);
    Task AddVilla(AddVillaRequest addVillaRequest);
    Task<VillaModel> UpdateVilla(int id, UpdateVillaRequest updateVillaRequest);
    Task DeleteVilla(int id);
}