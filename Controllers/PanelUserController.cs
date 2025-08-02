using System.Security.Claims;
using api_event_panel.Dtos;
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
    
    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> getAllPanelUsers()
    {
        var panelUserList = await _panelUserService.GetAllPanelUsers();
        return Ok(panelUserList);
    }

    [HttpGet]
    public async Task<IActionResult> GetPanelUser()
    {
        var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var panelUser = await _panelUserService.GetPanelUser(jwtUserId);
        return Ok(panelUser);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetPanelUser(int id)
    {
        var panelUser = await _panelUserService.GetPanelUser(id);
        return Ok(panelUser);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PostPanelUser(PanelUserPostRequest panelUser)
    {
        await _panelUserService.AddPanelUser(panelUser);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePanelUser(int id)
    {
        await _panelUserService.DeletePanelUser(id);
        return Ok();
    }

    [HttpGet("events/")]
    public async Task<IActionResult> GetPanelUserEvents()
    {
        var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        
        var eventList = await _eventService.GetPanelUserEvents(jwtUserId);
        return Ok(eventList);
    }
    [HttpGet("{id}/events/")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetPanelUserEventsById(int id)
    {
        var eventList = await _eventService.GetPanelUserEvents(id);
        return Ok(eventList);
    }

    [HttpPost("uploadProfilePicture")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadProfilePicture([FromForm] UploadProfilePictureRequest file)
    {
        var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

        if (file == null || file.File.Length == 0)
            return BadRequest("No file uploaded");
        await _panelUserService.UploadProfilePicture(jwtUserId, file.File);
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUser user)
    {
        var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

        await _panelUserService.UpdateUser(jwtUserId, user);
        return Ok();
    }

    [HttpPut("changePassword")]
    public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
    {
        var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var user = await _panelUserService.GetPanelUser(jwtUserId);
        
        if (BCrypt.Net.BCrypt.Verify(changePassword.oldPassword,user.Password))
        {
            var hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(changePassword.newPassword);
            user.Password =hashedNewPassword;
            await _panelUserService.UpdateUser(user);
            return Ok();
        }

        if (!BCrypt.Net.BCrypt.Verify(changePassword.newPassword, user.Password))
        {
            return BadRequest("Password doesn't match");
        }
        else
        {
            return BadRequest("Error changing password");
        }
    }
    
    
}