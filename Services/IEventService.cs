using api_event_panel.Dtos;
using api_event_panel.Models;

namespace api_event_panel.Services;

public interface IEventService
{
    Task SaveEvent(EventModelRequest eventModel);
    Task<List<EventModel>> GetEvents();
    Task <EventModel> GetEvent(int id);
    Task UpdateEvent(int id,UpdateEventRequest eventModel);
    Task DeleteEvent(int id);
    Task<List<EventModel>> GetPanelUserEvents(int panelUserId);
    
}