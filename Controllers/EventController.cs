using System.Security.Claims;
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
        var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        try
        {
            await _service.SaveEvent(jwtUserId, eventModel);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetEvents()
    {
        return Ok(await _service.GetEvents());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvent(int id)
    {
        try
        {
            var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;       

            return Ok(await _service.GetEvent(jwtUserId, id, userRole));
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
        
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEvent(int id, [FromBody] UpdateEventRequest updatedModel)
    {
        try
        {
            var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;       

            await _service.UpdateEvent(jwtUserId, id, updatedModel, userRole);
            return Ok(updatedModel);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
       
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
     
        try
        {
            var JwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;       

            var existingEvent = await _service.GetEvent(JwtUserId, id, userRole);
            if (existingEvent == null)
                return NotFound();

            await _service.DeleteEvent(id);
            return Ok();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
        
    }

    [HttpGet("{id}/users")]
    public async Task<IActionResult> GetEventUsers(int id)
    {
        try
        {
            var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;       

            var eventModel = await _service.GetEvent(jwtUserId, id,userRole);
            var userList = await _userRepository.GetUsersByEvent(eventModel.Id);
            return Ok(userList);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}/medias")]
    public async Task<IActionResult> GetEventMedias(int id)
    {
        try
        {
            var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;       

            var eventModel = await _service.GetEvent(jwtUserId, id, userRole);
            var mediaList = await _mediaRepository.GetMediasByEventId(eventModel.Id);
            return Ok(mediaList);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
       
    }
    
    
}