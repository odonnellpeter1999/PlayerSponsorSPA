using Microsoft.EntityFrameworkCore;
using PlayerSponsor.Server.Data.Context;
using PlayerSponsor.Server.Models;

namespace PlayerSponsor.Server.Repositories;

public class ClubRepository : Repository<Club>, IClubRepository
{
    public ClubRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Club?> GetBySlugAsync(string slug)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Slug == slug);
    }
}

