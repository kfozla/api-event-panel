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
        var servicePackage= await _servicePackageService.GetServicePackage(id);
        
        return Ok(servicePackage);
    }
    [HttpGet]
    public async Task<IActionResult> GetServicePackages()
    {
        var servicePackageList= await _servicePackageService.GetServicePackages();
        
        return Ok(servicePackageList);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PostServicePackage(ServicePackageRequest servicePackage)
    {
        await _servicePackageService.AddServicePackage(servicePackage);
        return Ok(servicePackage);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteServicePackage(int id)
    {
        await _servicePackageService.DeleteServicePackage(id);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutServicePackage(int id, ServicePackageRequest servicePackage)
    {
        await _servicePackageService.UpdateServicePackage(id,servicePackage);
        return Ok();
    }
}