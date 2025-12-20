using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayerSponsor.Server.Controllers.Requests;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Services.AccountService;
using PlayerSponsor.Server.Services.Commands;

[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IAuthorizationService _authorizationService;
    private readonly IAccountService _accountService;

    public UserController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IAuthorizationService authorizationService,
        IAccountService accountService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authorizationService = authorizationService;
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if the user is an admin for the specified club
        if (request.ClubId != null) {

            var authResult = await _authorizationService.AuthorizeAsync(User, request.ClubId, "ClubAdmin");

            if (!authResult.Succeeded)
            {
                return Forbid("You do not have permission to add users to this club");
            }
        }

        var registrationResult = await _accountService.RegisterUserAsync(new CreateUserCommand
        {
            Email = request.Email,
            Password = request.Password,
            ClubId = request.ClubId
        });

        if (!registrationResult.IsSuccess)
        {
            return BadRequest(registrationResult.Error);
        }

        await _signInManager.SignInAsync(registrationResult.Value, isPersistent: true);

        return Ok();
    }
}
