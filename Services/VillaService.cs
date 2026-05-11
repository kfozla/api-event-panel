using api_event_panel.Dtos;
using api_event_panel.Models;
using api_event_panel.Repositories;

namespace api_event_panel.Services;

public class VillaService: IVillaService
{
    private readonly IVillaRepository _villaRepository;

    public VillaService(IVillaRepository villaRepository)
    {
        _villaRepository = villaRepository;
    }

    public async Task AddVilla(AddVillaRequest addVillaRequest)
    {
        var villa = new VillaModel
        {
            Name = addVillaRequest.Name,
            Description = addVillaRequest.Description,
            CreatedOn = DateTime.Now,
            LastModifiedOn = DateTime.Now,
            CreatedBy = addVillaRequest.CreatedBy,
        };
        await  _villaRepository.AddVilla(villa);
    }

    public async Task DeleteVilla(int id)
    {
        await  _villaRepository.DeleteVilla(id);
    }

    public async Task<List<VillaModel>> GetAllVillas()
    {
        return await _villaRepository.GetAllVillas();
    }

    public async Task<VillaModel> GetVillaById(int id)
    {
        return await _villaRepository.GetVilla(id);
    }

    public async Task<VillaModel> UpdateVilla(int id, UpdateVillaRequest updateVillaRequest)
    {
        var villa = _villaRepository.GetVilla(id).Result;
        
        villa.Name = updateVillaRequest.Name;
        villa.Description = updateVillaRequest.Description;
        villa.CreatedOn = updateVillaRequest.CreatedOn;
        villa.LastModifiedOn = DateTime.Now;
        villa.CreatedBy = updateVillaRequest.CreatedBy;
       return await _villaRepository.UpdateVilla(villa);
    }
}