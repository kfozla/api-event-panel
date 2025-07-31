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

    [HttpGet("events/")]
    public async Task<IActionResult> GetPanelUserEvents()
    {
        var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        
        var eventList = await _eventService.GetPanelUserEvents(jwtUserId);
        return Ok(eventList);
    }

    [HttpPost("{panelUserId}/uploadProfilePicture")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadProfilePicture(int panelUserId,[FromForm] UploadProfilePictureRequest file)
    {
        if (file == null || file.File.Length == 0)
            return BadRequest("No file uploaded");
        await _panelUserService.UploadProfilePicture(panelUserId, file.File);
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUser user)
    {
        await _panelUserService.UpdateUser(id, user);
        return Ok();
    }

    [HttpPut("{id}/changePassword")]
    public async Task<IActionResult> ChangePassword(int id,ChangePassword changePassword)
    {
        var user = await _panelUserService.GetPanelUser(id);
        
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