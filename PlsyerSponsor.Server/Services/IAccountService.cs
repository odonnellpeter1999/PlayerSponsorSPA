using PlayerSponsor.Server.Common;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Services.DTOs;

namespace PlayerSponsor.Server.Services
{
    public interface IAccountService
    {
        Task<ResultT<ApplicationUser>> RegisterUserAsync(NewApplicationUser newUser);
    }
}