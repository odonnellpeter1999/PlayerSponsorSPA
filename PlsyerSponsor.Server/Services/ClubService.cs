using AutoMapper;
using PlayerSponsor.Server.Controllers.Requests;
using PlayerSponsor.Server.Controllers.Responses;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Repositories;

namespace PlayerSponsor.Server.Services;

public class ClubService : IClubService
{
    private readonly IClubRepository _clubRepository;
    private readonly IMapper _mapper;

    public ClubService(IClubRepository clubRepository, IMapper mapper)
    {
        _clubRepository = clubRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClubDto>> GetAllClubsAsync()
    {
        var clubs = await _clubRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ClubDto>>(clubs);
    }

    public async Task<ClubDto> GetClubByIdAsync(int id)
    {
        var club = await _clubRepository.GetByIdAsync(id);
        if (club == null)
            throw new Exception("Club not found");

        return _mapper.Map<ClubDto>(club);
    }

    public async Task<ClubDto> CreateClubAsync(CreateClubRequest clubDto)
    {
        var club = _mapper.Map<Club>(clubDto);

        club.PlayerKey = Guid.NewGuid().ToString();

        await _clubRepository.AddAsync(club);
        return _mapper.Map<ClubDto>(club);
    }

    public async Task<bool> UpdateClubAsync(int id, UpdateClubDto clubDto)
    {
        var club = await _clubRepository.GetByIdAsync(id);
        if (club == null)
            return false;

        _mapper.Map(clubDto, club);
        await _clubRepository.UpdateAsync(club);
        return true;
    }

    public async Task<bool> DeleteClubAsync(int id)
    {
        var club = await _clubRepository.GetByIdAsync(id);
        if (club == null)
            return false;

        await _clubRepository.DeleteAsync(id);
        return true;
    }
}

