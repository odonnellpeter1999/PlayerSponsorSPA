using PlayerSponsor.Server.Common;
using PlayerSponsor.Server.Controllers.Requests;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Services.DTOs;

namespace PlayerSponsor.Server.Services.ClubService;

public interface IClubService
{
    Task<ResultT<IEnumerable<ClubDto>>> GetAllClubsAsync();
    Task<ResultT<ClubDto>> GetClubByIdAsync(int id);
    Task<ResultT<Club>> CreateClub(Club newClub);
    Task<Result> UpdateClubAsync(Club club);
    Task<Result> DeleteClubAsync(int id);
    Task<ResultT<ClubAdmin>> AddClubAdminAsync(ClubAdmin newClubAdmin);
}
