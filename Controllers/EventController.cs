using api_event_panel.Data;
using api_event_panel.Dtos;
using api_event_panel.Models;
using api_event_panel.Repositories;
using api_event_panel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_event_panel.Controllers;

[Authorize]
[ApiController]
[Route("api/events")]
public class EventController:ControllerBase
{
    private readonly IEventService _service;
    private readonly IUserRepository _userRepository;
    private readonly IMediaRepository _mediaRepository;
  
    public EventController(IEventService service, IUserRepository userRepository, IMediaRepository mediaRepository)
    {
        _service = service;
        _userRepository = userRepository;
        _mediaRepository = mediaRepository;
    }

    [HttpPost]
    public async Task<IActionResult> PostEvent([FromBody]EventModelRequest eventModel)
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
    public async Task<IActionResult> PutEvent(int id, [FromBody] UpdateEventRequest updatedModel)
    { await _service.UpdateEvent(id,updatedModel);
        return Ok(updatedModel);
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

    [HttpGet("{id}/users")]
    public async Task<IActionResult> GetEventUsers(int id)
    {
        var userList = await _userRepository.GetUsersByEvent(id);
        return Ok(userList);
    }

    [HttpGet("{id}/medias")]
    public async Task<IActionResult> GetEventMedias(int id)
    {
        var mediaList = await _mediaRepository.GetMediasByEventId( id);
        return Ok(mediaList);
    }
    
}