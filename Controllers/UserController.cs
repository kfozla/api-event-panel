using System.Security.Claims;
using api_event_panel.Dtos;
using api_event_panel.Models;
using api_event_panel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api_event_panel.Controllers;


[ApiController]
[Route("api/user")]
public class UserController: ControllerBase
{
    private readonly IUserService _service;
    private readonly IMediaService _mediaService;

    public UserController(IUserService service, IMediaService mediaService)
    {
        _service = service;
        _mediaService = mediaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserModel>>> Get()
    {
        try
        {
            return Ok(await _service.GetAll());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel>> Get(int id)
    {
        try
        {
            return Ok(await _service.GetById(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
       
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] UserModelRequest user)
    {
        try
        {
            await _service.AddUser(user);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            await _service.DeleteUser(jwtUserId, id);
            return Ok();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpGet("event/{eventId}")]
    public async Task<ActionResult<List<UserModel>>> GetUsersByEvent(int eventId)
    {
        try
        {
            var userList = await _service.GetUsersByEvent(eventId);
            return Ok(userList);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpGet("{id}/mediaCount")]
    public async Task<ActionResult<List<UserModel>>> GetUserMediaCount(int id)
    {
        try
        {
            return Ok(await _service.GetUserMediaCount(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpGet("{id}/media")]
    public async Task<ActionResult<List<MediaModel>>> GetUserMediaList(int id)
    {
        try
        {
            return Ok(await _service.GetUserMediaList(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [RequestSizeLimit(1073741824)]//1Gb
    [HttpPost("{id}/media")]
    public async Task<ActionResult<MediaModel>> PostMedia([FromForm] string username,[FromForm] string sessionId, [FromForm] List<IFormFile> file)
    {
        if(file.Count == 0)
            return BadRequest("No media file found");
        try
        {
            await _mediaService.SaveMedia(username ,sessionId,file);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpGet("getByUsername/{username}/sessionId/{sessionId}")]
    public async Task<ActionResult<UserModel>> GetByUsernameAndSessionId(string username, string sessionId)
    {
        try
        {
            var user = await _service.GetUserByUsernameAndSessionId(username, sessionId);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

   
}