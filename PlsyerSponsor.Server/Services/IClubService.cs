using PlayerSponsor.Server.Controllers.Requests;
using PlayerSponsor.Server.Controllers.Responses;

namespace PlayerSponsor.Server.Services;

public interface IClubService
{
    Task<IEnumerable<ClubDto>> GetAllClubsAsync();
    Task<ClubDto> GetClubByIdAsync(int id);
    Task<ClubDto> CreateClubAsync(CreateClubRequest clubDto);
    Task<bool> UpdateClubAsync(int id, UpdateClubDto clubDto);
    Task<bool> DeleteClubAsync(int id);
}
