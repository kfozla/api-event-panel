using api_event_panel.Dtos;
using api_event_panel.Models;

namespace api_event_panel.Services;

public interface IEventService
{
    Task SaveEvent(int id,EventModelRequest eventModel);
    Task<List<UserMappedevent>> GetEvents();
    Task <EventModel> GetEvent(int jwtUserId,int id, string userRole);
    Task UpdateEvent(int jwtUserId, int id,UpdateEventRequest eventModel, string userRole);
    Task DeleteEvent(int id);
    Task<List<EventModel>> GetPanelUserEvents(int panelUserId);
    
}