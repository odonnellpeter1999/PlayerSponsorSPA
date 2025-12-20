using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlayerSponsor.Server.Common;
using PlayerSponsor.Server.Controllers.Requests;
using PlayerSponsor.Server.Controllers.Responses;
using PlayerSponsor.Server.Services.AccountService;
using PlayerSponsor.Server.Services.ClubService;
using PlayerSponsor.Server.Services.Commands;

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
    public async Task<IActionResult> GetClubById(int id)
    {
        var result = await _clubService.GetClubByIdAsync(id);

        if (!result.IsSuccess)
            return Problem(result.Error!);

        return Ok(result.Value);
    }

    // For Now Disabled in MVP - Club Registration,Updating and Deletion will be handled internally

    //[HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateClubRequest createClubRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Create the club
        var createClubCommand = _mapper.Map<CreateClubCommand>(createClubRequest);

        var createClubResult = await _clubService.CreateClub(createClubCommand);

        if (!createClubResult.IsSuccess)
            return Problem(createClubResult.Error!);

        // Create the admin account
        var createNewUserCommand = _mapper.Map<CreateUserCommand>(createClubRequest);

        createNewUserCommand.ClubId = createClubResult.Value.Id;

        var registerResult = await _accountService.RegisterUserAsync(createNewUserCommand);

        if (!registerResult.IsSuccess)
        {
            // Rollback club creation if admin account creation fails
            await _clubService.DeleteClubAsync(createClubResult.Value.Id);
            return Problem(registerResult.Error!);
        }

        return Ok(new CreateClubResponse() { ClubId = createClubResult.Value.Id.ToString() });
    }

    //[HttpPut("{id}")]
    public async Task<IActionResult> UpdateClub(int id, [FromBody] UpdateClubDetailsRequest updateClubRequest)
    {
        var updateClubDetailsCommand = _mapper.Map<UpdateClubDetailsCommand>(updateClubRequest);

        updateClubDetailsCommand.Id = id;

        var result = await _clubService.UpdateClubDetails(updateClubDetailsCommand);

        if (!result.IsSuccess)
            return Problem(result.Error!);

        return NoContent();
    }

    //[HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClub(int id)
    {
        var result = await _clubService.DeleteClubAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.Error);

        return NoContent();
    }
}

