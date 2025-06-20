using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PlayerSponsor.Server.Data.Context;
using PlayerSponsor.Server.Models;
using System.Security.Claims;

namespace PlayerSponsor.Server.Auth;


public class ClubAdminClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public ClubAdminClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
        IOptions<IdentityOptions> optionsAccessor, ApplicationDbContext dbContext)
        : base(userManager, optionsAccessor)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        try
        {
            var identity = await base.GenerateClaimsAsync(user);

            // Fetch club admin memberships
            var clubIds = _dbContext.Admins
                .Where(ca => ca.User.Id == user.Id)
                .Select(ca => ca.Club.Id.ToString())
                .ToList();

            foreach (var clubId in clubIds)
            {
                identity.AddClaim(new Claim("ClubAdmin", clubId));
            }

            return identity;

        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while generating claims.", ex);
        }
    }
}
