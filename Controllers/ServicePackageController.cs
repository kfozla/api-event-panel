using api_event_panel.Dtos;
using api_event_panel.Models;
using api_event_panel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_event_panel.Controllers;
[ApiController]
[Authorize]
[Route("api/servicePackage")]
public class ServicePackageController: ControllerBase
{
    private readonly IServicePackageService _servicePackageService;

    public ServicePackageController(IServicePackageService servicePackageService)
    {
        _servicePackageService = servicePackageService;;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetServicePackage(int id)
    {
        try
        {
            var servicePackage = await _servicePackageService.GetServicePackage(id);
            return Ok(servicePackage);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetServicePackages()
    {
        try
        {
            var servicePackageList = await _servicePackageService.GetServicePackages();

            return Ok(servicePackageList);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PostServicePackage(ServicePackageRequest servicePackage)
    {
        try
        {
            await _servicePackageService.AddServicePackage(servicePackage);
            return Ok(servicePackage);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteServicePackage(int id)
    {
        try
        {
            await _servicePackageService.DeleteServicePackage(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutServicePackage(int id, ServicePackageRequest servicePackage)
    {
        try
        {
            await _servicePackageService.UpdateServicePackage(id, servicePackage);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
}