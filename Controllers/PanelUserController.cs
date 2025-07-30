using api_event_panel.Models;
using api_event_panel.Repositories;
using api_event_panel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_event_panel.Controllers;

[Authorize]
[ApiController]
[Route("api/panelUsers")]
public class PanelUserController:ControllerBase
{
    private readonly IPanelUserService _panelUserService;
    private readonly IEventService _eventService;


    public PanelUserController(IPanelUserService panelUserRepository, IEventService eventService)
    {
        _panelUserService = panelUserRepository;
        _eventService=eventService;;
    }
    
    [HttpGet]
    public async Task<IActionResult> getAllPanelUsers()
    {
        var panelUserList = await _panelUserService.GetAllPanelUsers();
        return Ok(panelUserList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPanelUser(int id)
    {
        var panelUser = await _panelUserService.GetPanelUser(id);
        return Ok(panelUser);
    }

    [HttpPost]
    public async Task<IActionResult> PostPanelUser(PanelUserModel panelUser)
    {
        await _panelUserService.AddPanelUser(panelUser);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePanelUser(int id)
    {
        await _panelUserService.DeletePanelUser(id);
        return Ok();
    }

    [HttpGet("events/{panelUserId}")]
    public async Task<IActionResult> GetPanelUserEvents(int panelUserId)
    {
        var eventList = await _eventService.GetPanelUserEvents(panelUserId);
        return Ok(eventList);
    }

    [HttpPost("{panelUserId}/uploadProfilePicture")]
    public async Task<IActionResult> UploadProfilePicture(int panelUserId,[FromForm] IFormFile file)
    {
        await _panelUserService.UploadProfilePicture(panelUserId, file);
        return Ok();
    }
    
}