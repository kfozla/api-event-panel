using api_event_panel.Models;

namespace api_event_panel.Repositories;

public interface IRandevuRepository
{
    Task Add(Randevu randevu);
    Task Update(Randevu randevu);
    Task Delete(int id);
    Task<List<Randevu>> GetAll();
    Task<Randevu> GetById(int id);
    Task<List<Randevu>> GetByPanelUserId(int panelUserId);
}