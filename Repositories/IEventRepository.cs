namespace api_event_panel.Repositories;
using Models;

public interface IEventRepository
{
    Task SaveEvent(EventModel eventModel);
    Task<EventModel> GetEvent(int id);
    Task<List<EventModel>> GetEvents();
    Task UpdateEvent(EventModel eventModel);
    Task DeleteEvent(int id);

}