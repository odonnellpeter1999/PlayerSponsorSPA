using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayerSponsor.Server.Common;
using PlayerSponsor.Server.Controllers.Requests;
using PlayerSponsor.Server.Controllers.Responses;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Services;
using PlayerSponsor.Server.Services.ClubService;
using PlayerSponsor.Server.Services.DTOs;

namespace PlayerSponsor.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClubController : BaseController
{
    private readonly IClubService _clubService;
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public ClubController(IClubService clubService, IMapper mapper, IAccountService accountService)
    {
        _clubService = clubService;
        _mapper = mapper;
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClubs()
    {
        var result = await _clubService.GetAllClubsAsync();

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    [Authorize("ClubAdminPolicy")]
    public async Task<IActionResult> GetClubById(int id)
    {
        var result = await _clubService.GetClubByIdAsync(id);

        if (!result.IsSuccess)
            return Problem(result.Error!);

        return Ok(result.Value);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateClubRequest clubRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Create the club
        var newClub = _mapper.Map<Club>(clubRequest);

        var createClubResult = await _clubService.CreateClub(newClub);

        if (!createClubResult.IsSuccess)
            return Problem(createClubResult.Error!);

        // Create the admin account
        var newUser = _mapper.Map<NewApplicationUser>(clubRequest);

        newUser.ClubId = createClubResult.Value.Id;

        var registerResult = await _accountService.RegisterUserAsync(newUser);

        if (!registerResult.IsSuccess)
        {
            // Rollback club creation if admin account creation fails
            await _clubService.DeleteClubAsync(createClubResult.Value.Id);
            return Problem(registerResult.Error!);
        }

        return Ok(new CreateClubResponse() {ClubId = createClubResult.Value.Id.ToString() });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClub(int id, [FromBody] UpdateClubRequest clubDto)
    {
        var club = _mapper.Map<Club>(clubDto);

        club.Id = id;

        var result = await _clubService.UpdateClubAsync(club);

        if (!result.IsSuccess)
            return Problem(result.Error!);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClub(int id)
    {
        var result = await _clubService.DeleteClubAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.Error);

        return NoContent();
    }
}

