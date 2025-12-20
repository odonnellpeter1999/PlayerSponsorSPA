using PlayerSponsor.Server.Common;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Services.Commands;

namespace PlayerSponsor.Server.Services.AccountService
{
    public interface IAccountService
    {
        Task<ResultT<ApplicationUser>> RegisterUserAsync(CreateUserCommand newUser);
    }
}