using api_event_panel.Dtos;
using api_event_panel.Models;
using api_event_panel.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api_event_panel.Services;

public class EventService: IEventService
{
    public readonly IEventRepository _repository;

    public EventService(IEventRepository eventRepository)
    {
        _repository = eventRepository;
    }

    public async Task SaveEvent(EventModelRequest eventModelRequest)
    {
        EventModel eventModel = new EventModel()
        {
            Name = eventModelRequest.name,
            Description = eventModelRequest.description,
            StartTime = eventModelRequest.startTime,
            EndTime = eventModelRequest.endTime,
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now,
            Theme = eventModelRequest.theme,
            PanelUserId = eventModelRequest.panelUserId,
            thumbnailUrl = eventModelRequest.thumbnailUrl,
            DomainName = eventModelRequest.domainName,
        };
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

    public async Task UpdateEvent(int id,UpdateEventRequest updateEventRequest)
    {
        EventModel eventModel = _repository.GetEvent(id).Result;

        eventModel.Name = updateEventRequest.name;
        eventModel.Description = updateEventRequest.description;
        eventModel.StartTime = updateEventRequest.startTime;
        eventModel.EndTime = updateEventRequest.endTime;
        eventModel.ModifiedOn = DateTime.Now;
        eventModel.Theme = updateEventRequest.theme;
        eventModel.PanelUserId = updateEventRequest.panelUserId;
        eventModel.thumbnailUrl = updateEventRequest.thumbnailUrl;
        eventModel.DomainName = updateEventRequest.domainName;
        await _repository.UpdateEvent(eventModel);
    }

    public async Task DeleteEvent(int id)
    {
        await _repository.DeleteEvent(id);
    }

    public async Task<List<EventModel>> GetPanelUserEvents(int panelUserId)
    {
        return await _repository.GetPanelUserEvents(panelUserId);
    }
    
}