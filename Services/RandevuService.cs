using api_event_panel.Data;
using api_event_panel.Dtos;
using api_event_panel.Models;
using api_event_panel.Repositories;

namespace api_event_panel.Services;

public class RandevuService:IRandevuService
{
    private readonly IRandevuRepository _randevuRepository;
    private readonly IPanelUserRepository _panelUserRepository;

    public RandevuService(IRandevuRepository randevuRepository, IPanelUserRepository panelUserRepository)
    {
        _randevuRepository = randevuRepository;
        _panelUserRepository = panelUserRepository;
    }

    public async Task Add(int jwtUserId,RandevuRequest randevu)
    {
        var panelUser = await _panelUserRepository.GetPanelUser(jwtUserId);
        Randevu Randevu = new ()
        {
            Name = randevu.name,
            start = randevu.start,
            end = randevu.end,
            description = randevu.description,
            panelUserId = panelUser.Id,
            createdOn = randevu.createdOn,
            updatedOn = DateTime.Now,
            deletedOn = null,
        };
        
        await _randevuRepository.Add(Randevu);
    }

    public async Task Update(int jwtUserId,int id,RandevuRequest randevu)
    { 
        var  panelUser = await _panelUserRepository.GetPanelUser(jwtUserId);
        var Randevu = await _randevuRepository.GetById(id);

        if (panelUser.Id != Randevu.panelUserId)
        {
         throw new UnauthorizedAccessException();
        }
        Randevu.Name = randevu.name;
        Randevu.start = randevu.start;
        Randevu.end = randevu.end;
        Randevu.description = randevu.description;
        Randevu.panelUserId = panelUser.Id;
        Randevu.createdOn = randevu.createdOn;
        Randevu.updatedOn = DateTime.Now;
       
        await _randevuRepository.Update(Randevu);
    }

    public async Task Delete(int id)
    {
        try
        {
            var Randevu = await _randevuRepository.GetById(id);

            await _randevuRepository.Delete(Randevu.Id);
        }
        catch (Exception e)
        {
           throw new Exception($"Delete Randevu failed. Id: {id}", e);
        }
        
    }

    public async Task<List<Randevu>> GetAll()
    {
        return await _randevuRepository.GetAll();
    }

    public async Task<Randevu> GetById(int id)
    {
        return await _randevuRepository.GetById(id);
    }

    public async Task<List<Randevu>> GetByPanelUserId(int jwtUserId, int id)
    {
        var  panelUser = _panelUserRepository.GetPanelUser(id).Result;
        if(panelUser.Id != jwtUserId)
            throw new UnauthorizedAccessException();
        
        var randevuList = await _randevuRepository.GetByPanelUserId(panelUser.Id);
        return randevuList;
    }
    
}