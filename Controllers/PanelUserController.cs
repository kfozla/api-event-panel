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
    private readonly IRandevuService _randevuService;

    public PanelUserController(IPanelUserService panelUserRepository, IEventService eventService, IRandevuService randevuService)
    {
        _panelUserService = panelUserRepository;
        _eventService=eventService;
        _randevuService=randevuService;
        
    }
    
    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> getAllPanelUsers()
    {
        try
        {
            var panelUserList = await _panelUserService.GetAllPanelUsers();
            return Ok(panelUserList);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpGet]
    public async Task<IActionResult> GetPanelUser()
    {
        try
        {
            var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var panelUser = await _panelUserService.GetPanelUser(jwtUserId);
            return Ok(panelUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetPanelUser(int id)
    {
        try
        {
            var panelUser = await _panelUserService.GetPanelUser(id);
            return Ok(panelUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PostPanelUser(PanelUserPostRequest panelUser)
    {
        try
        {
            await _panelUserService.AddPanelUser(panelUser);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePanelUser(int id)
    {
        try
        {
            await _panelUserService.DeletePanelUser(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("events/")]
    public async Task<IActionResult> GetPanelUserEvents()
    {
        try
        {
            var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var eventList = await _eventService.GetPanelUserEvents(jwtUserId);
            return Ok(eventList);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
    [HttpGet("{id}/events/")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetPanelUserEventsById(int id)
    {
        try
        {
            var eventList = await _eventService.GetPanelUserEvents(id);
            return Ok(eventList);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPost("uploadProfilePicture")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadProfilePicture([FromForm] UploadProfilePictureRequest file)
    {
        try
        {
            var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (file == null || file.File.Length == 0)
                return BadRequest("No file uploaded");
            await _panelUserService.UploadProfilePicture(jwtUserId, file.File);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUser user)
    {
        try
        {
            var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            await _panelUserService.UpdateUser(jwtUserId, user);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPut("changePassword")]
    public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
    {
        try
        {
            var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var user = await _panelUserService.GetPanelUser(jwtUserId);

            if (BCrypt.Net.BCrypt.Verify(changePassword.oldPassword, user.Password))
            {
                var hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(changePassword.newPassword);
                user.Password = hashedNewPassword;
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
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}/changeServicePackage")]
    public async Task<IActionResult> ChangeServicePackage(int id, int servicePackageId)
    {
        var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        try
        {
            await _panelUserService.ChangeServicePackage(jwtUserId, id, servicePackageId);
            return Ok();

        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        
    }

    [HttpGet("{id}/randevular")]
    public async Task<IActionResult> Randevular(int id)
    {
        try
        {
            var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            return Ok(await _randevuService.GetByPanelUserId(jwtUserId, id));
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
    
    
}