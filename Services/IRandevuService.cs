using api_event_panel.Dtos;
using api_event_panel.Models;

namespace api_event_panel.Services;

public interface IRandevuService
{
    Task<List<Randevu>> GetAll();
    Task<Randevu> GetById(int jwtUserid,int id, string userRole);
    Task Add(int jwtUserId,RandevuRequest randevu);
    Task Update(int jwtUserId,int id,RandevuRequest randevu);
    Task Delete(int jwtUserid,int id);
    Task<List<Randevu>> GetByPanelUserId(int panelUserId,int id);
}