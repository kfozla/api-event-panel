using System.Security.Claims;
using api_event_panel.Repositories;
using api_event_panel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api_event_panel.Controllers;


[ApiController]
[Route("api/medias")]
public class MediaController: ControllerBase
{
    private readonly IMediaService _service;

    public MediaController(IMediaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetMedias()
    {
        try
        {
            return Ok(await _service.GetMedias());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
    
    [RequestSizeLimit(1073741824)]//1Gb
    [HttpPost("upload")]
    public async Task<IActionResult> SaveMedia([FromForm] string username,[FromForm] string sessionId,[FromForm] List<IFormFile> file )
    {
        if (file.Count == 0 )
            return BadRequest("No media file found");
        try
        {
            await _service.SaveMedia(username,sessionId, file);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMedia(int id)
    {
        try
        {
            return Ok(await _service.GetMedia(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
      
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUser(int userId)
    {
        try
        {
            return Ok(await _service.GetMediaByUserId(userId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
       
    }

    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetMediaByUserId([FromQuery] int eventId)
    {
        try
        {
            return Ok(await _service.GetMediasByEventId(eventId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedia(int id,string username,string sessionId)
    {
        var jwtUserRole = User.FindFirst(ClaimTypes.Role)?.Value;       
        try
        {
            await _service.DeleteMedia(username, sessionId, id, jwtUserRole);
            return Ok();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
}