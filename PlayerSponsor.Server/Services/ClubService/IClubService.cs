using PlayerSponsor.Server.Common;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Services.Commands;
using PlayerSponsor.Server.Services.DTOs;

namespace PlayerSponsor.Server.Services.ClubService;

public interface IClubService
{
    Task<ResultT<IEnumerable<ClubDto>>> GetAllClubsAsync();
    Task<ResultT<ClubDto>> GetClubByIdAsync(int id);
    Task<ResultT<ClubDto>> CreateClub(CreateClubCommand newClub);
    Task<Result> UpdateClubDetails(UpdateClubDetailsCommand club);
    Task<Result> DeleteClubAsync(int id);
    Task<ResultT<ClubAdmin>> AddClubAdminAsync(ClubAdmin newClubAdmin);
    Task<ResultT<ClubDto>> GetClubBySlugAsync(string slug);
}
