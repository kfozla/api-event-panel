using api_event_panel.Dtos;
using api_event_panel.Models;
using api_event_panel.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api_event_panel.Services;

public class EventService: IEventService
{
    public readonly IEventRepository _repository;
    public readonly IPanelUserRepository _panelUserRepository;

    public EventService(IEventRepository eventRepository, IPanelUserRepository panelUserRepository)
    {
        _repository = eventRepository;
        _panelUserRepository = panelUserRepository;
    }

    public async Task SaveEvent(int id,EventModelRequest eventModelRequest)
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
            PanelUserId =id,
            thumbnailUrl = eventModelRequest.thumbnailUrl,
            DomainName = eventModelRequest.domainName,
        };
        await _repository.SaveEvent(eventModel);
       
    }
    public async Task<List<UserMappedevent>> GetEvents()
    {
        List<EventModel> events = await _repository.GetEvents();
        List<UserMappedevent> mappedEvents = new();

        foreach (var e in events)
        {
            var panelUser = await _panelUserRepository.GetPanelUser(e.PanelUserId);
    
            mappedEvents.Add(new UserMappedevent
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                CreatedOn = e.CreatedOn,
                Theme = e.Theme,
                PanelUserId = e.PanelUserId,
                thumbnailUrl = e.thumbnailUrl,
                DomainName = e.DomainName,
                ModifiedOn = e.ModifiedOn,
                DeletedOn = e.DeletedOn,
                UserList = e.UserList,
                PanelUser = panelUser
            });
        }
        
        return mappedEvents;
    }

    public async Task<EventModel> GetEvent(int jwtUserId,int id,string userRole)
    {
        
        var eventModel = await _repository.GetEvent(id);
        if (userRole == "Admin")
        {
            return eventModel;
        }
        if (jwtUserId == eventModel.PanelUserId)
        {
            return eventModel;
        }
        else
        {   
            throw new UnauthorizedAccessException("Unauthorized");
        }
        
    }

    public async Task UpdateEvent(int jwtUserId,int id,UpdateEventRequest updateEventRequest,string userRole)
    {
        EventModel eventModel = GetEvent(jwtUserId,id,userRole).Result;
        
        eventModel.Name = updateEventRequest.name;
        eventModel.Description = updateEventRequest.description;
        eventModel.StartTime = updateEventRequest.startTime;
        eventModel.EndTime = updateEventRequest.endTime;
        eventModel.ModifiedOn = DateTime.Now;
        eventModel.Theme = updateEventRequest.theme;
        eventModel.PanelUserId = jwtUserId;
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