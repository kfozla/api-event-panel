using api_event_panel.Data;
using api_event_panel.Models;
using api_event_panel.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using  api_event_panel.Dtos;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;


namespace api_event_panel.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController:ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwtService;

    public AuthController(AppDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.PanelUsers.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user == null)
            return Unauthorized("Invalid credentials");

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
        if (!isPasswordValid)
            return Unauthorized("Invalid credentials");

        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshTokenValue = _jwtService.GenerateRefreshToken();

        var newRefreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshTokenValue,
            Expires = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            Created = DateTime.UtcNow
        };

        _context.RefreshTokens.Add(newRefreshToken);
        await _context.SaveChangesAsync();

        // Şimdi eski tokenları sil
        var oldTokens = _context.RefreshTokens
            .Where(rt => rt.UserId == user.Id && rt.Id != newRefreshToken.Id);

        _context.RefreshTokens.RemoveRange(oldTokens);
        await _context.SaveChangesAsync();

        return Ok(new { accessToken, refreshToken = refreshTokenValue });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var token = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == request.RefreshToken && !r.IsRevoked);

        if (token == null || token.Expires < DateTime.UtcNow)
            return Unauthorized("Invalid or expired refresh token");

        var user = await _context.PanelUsers.FindAsync(token.UserId);
        var newAccessToken = _jwtService.GenerateAccessToken(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        token.IsRevoked = true;

        _context.RefreshTokens.Add(new RefreshToken
        {
            UserId = user.Id,
            Token = newRefreshToken,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        await _context.SaveChangesAsync();

        return Ok(new { accessToken = newAccessToken, refreshToken = newRefreshToken });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        var token = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == request.RefreshToken);
        if (token != null)
        {
            token.IsRevoked = true;
            await _context.SaveChangesAsync();
        }

        return Ok();
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        // Örnek: Kullanıcı adı daha önce alınmış mı kontrol et
        if (await _context.PanelUsers.AnyAsync(u => u.Username == request.Username))
            return BadRequest("Username already exists");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new PanelUserModel
        {
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = hashedPassword,
            Role = "User",  
            CreatedOn = DateTime.UtcNow,
            ModifiedOn = DateTime.UtcNow
        };

        _context.PanelUsers.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully");
    }

    [HttpGet("user/{username}")]
    public async Task<IActionResult> GetUser(string username)
    {
        var user = await _context.PanelUsers.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null)
            return Unauthorized("Invalid username");
        return Ok(user);
    }

    
}