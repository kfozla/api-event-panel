using api_event_panel.Dtos;
using api_event_panel.Models;
using api_event_panel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api_event_panel.Controllers;

[Authorize]
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
        return Ok(await _service.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel>> Get(int id)
    {
        return Ok( await _service.GetById(id));
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] UserModelRequest user)
    {
        await _service.AddUser(user);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.DeleteUser(id);
        return Ok();
    }

    [HttpGet("event/{eventId}")]
    public async Task<ActionResult<List<UserModel>>> GetUsersByEvent(int eventId)
    {
        var userList = await _service.GetUsersByEvent(eventId);
        return Ok(userList);
    }

    [HttpGet("{id}/mediaCount")]
    public async Task<ActionResult<List<UserModel>>> GetUserMediaCount(int id)
    {
        return Ok(await _service.GetUserMediaCount(id));
    }

    [HttpGet("{id}/media")]
    public async Task<ActionResult<List<MediaModel>>> GetUserMediaList(int id)
    {
        return Ok(await _service.GetUserMediaList(id));
    }

    [HttpPost("{id}/media")]
    public async Task<ActionResult<MediaModel>> PostMedia(int id, [FromForm] List<IFormFile> media)
    {
        if(media.Count == 0)
            return BadRequest();
        
        await _mediaService.SaveMedia(id, media);
        return Ok();
    }

   
}