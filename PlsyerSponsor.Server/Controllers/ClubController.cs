using Microsoft.AspNetCore.Mvc;
using PlayerSponsor.Server.Controllers.Requests;
using PlayerSponsor.Server.Controllers.Responses;
using PlayerSponsor.Server.Services;

namespace PlayerSponsor.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClubController : ControllerBase
{
    private readonly IClubService _clubService;

    public ClubController(IClubService clubService)
    {
        _clubService = clubService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClubDto>>> GetAllClubs()
    {
        var clubs = await _clubService.GetAllClubsAsync();
        return Ok(clubs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClubDto>> GetClubById(int id)
    {
        try
        {
            var club = await _clubService.GetClubByIdAsync(id);
            return Ok(club);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<ClubDto>> CreateClub([FromBody] CreateClubRequest clubDto)
    {
        var createdClub = await _clubService.CreateClubAsync(clubDto);
        return CreatedAtAction(nameof(GetClubById), new { id = createdClub.Id }, createdClub);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClub(int id, [FromBody] UpdateClubDto clubDto)
    {
        var updated = await _clubService.UpdateClubAsync(id, clubDto);
        if (!updated)
            return NotFound(new { message = "Club not found" });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClub(int id)
    {
        var deleted = await _clubService.DeleteClubAsync(id);
        if (!deleted)
            return NotFound(new { message = "Club not found" });

        return NoContent();
    }
}

