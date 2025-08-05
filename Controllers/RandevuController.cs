using System.Security.Claims;
using api_event_panel.Dtos;
using api_event_panel.Models;
using api_event_panel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_event_panel.Controllers;

[ApiController]
[Route("api/randevu")]
[Authorize]
public class RandevuController: ControllerBase
{
    private readonly IRandevuService _randevuService;

    public RandevuController(IRandevuService randevuService)
    {
        _randevuService = randevuService;
    }

    [HttpGet]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _randevuService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _randevuService.GetById(id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _randevuService.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        
    }

    [HttpPost]
    public async Task<IActionResult> Post(RandevuRequest randevu)
    {
        try
        {
            var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            await _randevuService.Add(jwtUserId, randevu);
            return Ok();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RandevuRequest randevu)
    {
        try
        {
            var jwtUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            await _randevuService.Update(jwtUserId, id, randevu);
            return Ok();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
}