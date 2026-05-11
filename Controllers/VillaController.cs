using api_event_panel.Dtos;
using api_event_panel.Models;
using api_event_panel.Repositories;
using api_event_panel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_event_panel.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VillaController: ControllerBase
{
    private readonly IVillaService _villaService;

    public VillaController(IVillaService villaService)
    {
        _villaService = villaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetVillas()
    {
        return Ok( await _villaService.GetAllVillas());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVilla(int id)
    {
        return Ok( await _villaService.GetVillaById(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddVilla(AddVillaRequest addVillaRequest)
    {
        await _villaService.AddVilla(addVillaRequest);
        return Ok ();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVilla(int id,UpdateVillaRequest updateVillaRequest)
    {
        var villa = await _villaService.GetVillaById(id);
        if (villa == null  ) return NotFound("Villa not found");
        
        await _villaService.UpdateVilla(id, updateVillaRequest);
        return Ok();
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVilla(int id)
    {
        await _villaService.DeleteVilla(id);
        return Ok();
    }

   
    
}