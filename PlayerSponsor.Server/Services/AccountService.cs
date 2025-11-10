using Microsoft.AspNetCore.Identity;
using PlayerSponsor.Server.Common;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Services.ClubService;
using PlayerSponsor.Server.Services.DTOs;

namespace PlayerSponsor.Server.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IClubService _clubService;

    public AccountService(UserManager<ApplicationUser> userManager, IClubService clubService, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _clubService = clubService;
        _signInManager = signInManager;
    }

    public async Task<ResultT<ApplicationUser>> RegisterUserAsync(NewApplicationUser newUser)
    {
        var user = new ApplicationUser
        {
            UserName = newUser.Email,
            Email = newUser.Email
        };

        var result = await _userManager.CreateAsync(user, newUser.Password);

        if (!result.Succeeded)
        {
            var errorMessage = result?.Errors?.FirstOrDefault()?.Description ?? "Error creating your account";

            return Error.Validation("Account.Registration", errorMessage);
        }

        if (newUser.ClubId.HasValue)
        {
            var newClubAdmin = new ClubAdmin
            {
                UserId = user.Id,
                ClubId = newUser.ClubId.Value
            };

            await _clubService.AddClubAdminAsync(newClubAdmin);
        }

        return user;
    }
}
