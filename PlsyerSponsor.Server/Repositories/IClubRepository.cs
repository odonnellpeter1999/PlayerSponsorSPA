using PlayerSponsor.Server.Models;

namespace PlayerSponsor.Server.Repositories;

public interface IClubRepository : IRepository<Club>
{
    Task<Club> GetClubWithTeamsAndPlayersAsync(int clubId);
}

