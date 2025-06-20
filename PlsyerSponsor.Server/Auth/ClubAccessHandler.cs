using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace PlayerSponsor.Server.Auth;

public class ClubAccessRequirement : IAuthorizationRequirement
{
    public ClubAccessRequirement() { }
}

public class ClubAccessHandler : AuthorizationHandler<ClubAccessRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClubAccessHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ClubAccessRequirement requirement)
    {
        var user = context.User;

        var clubIds = user.FindAll("ClubAdmin").Select(id => id.Value);

        var routeValues = _httpContextAccessor.HttpContext?.Request.RouteValues;
        var requestedClubId = routeValues?["clubId"]?.ToString();

        if (!clubIds.IsNullOrEmpty() && requestedClubId != null && clubIds.Contains(requestedClubId))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        context.Fail();
        return Task.CompletedTask;
    }
}