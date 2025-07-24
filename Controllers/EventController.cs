using api_event_panel.Data;
using api_event_panel.Models;
using api_event_panel.Repositories;
using api_event_panel.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_event_panel.Controllers;

[ApiController]
[Route("api/events")]
public class EventController:ControllerBase
{
    private readonly IEventService _service;
  
    public EventController(IEventService service)
    {
        _service = service;
      
    }

    [HttpPost]
    public async Task<IActionResult> PostEvent(EventModel eventModel)
    {
        await _service.SaveEvent(eventModel);
        return Ok();
    }
    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        return Ok(await _service.GetEvents());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvent(int id)
    {
        return Ok(await _service.GetEvent(id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEvent(int id, [FromBody] EventModel updatedModel)
    {
        var existingEvent = await _service.GetEvent(id);
        if (existingEvent == null)
            return NotFound();

        existingEvent.Name = updatedModel.Name;
        existingEvent.Description = updatedModel.Description;
        existingEvent.StartTime = updatedModel.StartTime;
        existingEvent.EndTime = updatedModel.EndTime;
        existingEvent.thumbnailUrl = updatedModel.thumbnailUrl;
        existingEvent.ModifiedOn = DateTime.UtcNow;

        await _service.UpdateEvent(existingEvent);
        return Ok(existingEvent);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var existingEvent = await _service.GetEvent(id);
        if (existingEvent == null)
            return NotFound();

        await _service.DeleteEvent(id);
        return Ok();
    }

    
    
}