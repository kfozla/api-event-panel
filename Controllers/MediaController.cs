using api_event_panel.Repositories;
using api_event_panel.Services;
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
        return Ok(await _service.GetMedias());
    }
    
    [RequestSizeLimit(1073741824)]//1Gb
    [HttpPost("upload")]
    public async Task<IActionResult> SaveMedia([FromForm] int userId,[FromForm] List<IFormFile> mediaList )
    {
        if (mediaList.Count == 0 )
            return BadRequest();
        await _service.SaveMedia(userId, mediaList);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMedia(int id)
    {
        return Ok(await _service.GetMedia(id));
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUser([FromQuery]int userId)
    {
       return Ok( await _service.GetMediaByUserId(userId));
    }

    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetMediaByUserId([FromQuery] int eventId)
    {
        return Ok(await _service.GetMediasByEventId(eventId));
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> DeleteMedia(int id)
    {
        await _service.DeleteMedia(id);
        return Ok();
    }
}