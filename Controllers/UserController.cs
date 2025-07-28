using api_event_panel.Models;
using api_event_panel.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api_event_panel.Controllers;
[ApiController]
[Route("api/user")]
public class UserController: ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
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
    public async Task<ActionResult> Post([FromBody] UserModel user)
    {
        await _service.AddUser(user);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var user = await _service.GetById(id);
        await _service.DeleteUser(id);
        return Ok(user);
    }

    [HttpGet("event/{eventId}")]
    public async Task<ActionResult<List<UserModel>>> GetUsersByEvent(int eventId)
    {
        var userList = await _service.GetUsersByEvent(eventId);
        return Ok(userList);
    }
}