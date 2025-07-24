using api_event_panel.Models;
using api_event_panel.Repositories;

namespace api_event_panel.Services;

public class EventService: IEventService
{
    public readonly IEventRepository _repository;

    public EventService(IEventRepository eventRepository)
    {
        _repository = eventRepository;
    }

    public async Task SaveEvent(EventModel eventModel)
    {
        eventModel.CreatedOn = DateTime.UtcNow;
        eventModel.ModifiedOn = DateTime.UtcNow;
        await _repository.SaveEvent(eventModel);
    }
    public async Task<List<EventModel>> GetEvents()
    {
        return await _repository.GetEvents();
    }

    public async Task<EventModel> GetEvent(int id)
    {
        return await _repository.GetEvent(id);
    }

    public async Task UpdateEvent(EventModel eventModel)
    {
        await _repository.UpdateEvent(eventModel);
    }

    public async Task DeleteEvent(int id)
    {
        await _repository.DeleteEvent(id);
    }
}