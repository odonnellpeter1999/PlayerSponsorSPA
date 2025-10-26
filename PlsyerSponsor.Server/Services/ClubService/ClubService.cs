using AutoMapper;
using PlayerSponsor.Server.Common;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Repositories;
using PlayerSponsor.Server.Services.DTOs;

namespace PlayerSponsor.Server.Services.ClubService;

public class ClubService(IClubRepository clubRepository, IMapper mapper, IClubAdminRepository adminRepository, ILogger<ClubService> logger) : IClubService
{
    private readonly IClubRepository _clubRepository = clubRepository;
    private readonly IClubAdminRepository _adminRepository = adminRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ClubService> _logger = logger;

    public async Task<ResultT<IEnumerable<ClubDto>>> GetAllClubsAsync()
    {
        var clubs = await _clubRepository.GetAllAsync();

        var clubDtos = _mapper.Map<IEnumerable<ClubDto>>(clubs);

        return ResultT<IEnumerable<ClubDto>>.Success(clubDtos);
    }

    public async Task<ResultT<ClubDto>> GetClubByIdAsync(int id)
    {
        var club = await _clubRepository.GetByIdAsync(id);

        if (club is null)
            return ClubServiceError.NotFound(id.ToString());

        var clubDto = _mapper.Map<ClubDto>(club);

        return ResultT<ClubDto>.Success(clubDto);
    }

    public async Task<ResultT<Club>> CreateClub(Club newClub)
    {
        var validationResult = IsClubValid(newClub);
        if (!validationResult.IsSuccess)
        {
            return ResultT<Club>.Failure(validationResult.Error ?? Error.Validation("Club.Validation", "Error creating your club"));
        }

        newClub.Logo = "";
        newClub.PlayerKey = Guid.NewGuid().ToString();

        var club = await _clubRepository.AddAsync(newClub);

        if (club == null)
        {
            _logger.LogError("Failed to create club");
            return ClubServiceError.CreateFailure;
        }

        return ResultT<Club>.Success(club);
    }

    private ResultT<bool> IsClubValid(Club newClub)
    {
        if (string.IsNullOrWhiteSpace(newClub.Name))
        {
            _logger.LogError("Club name is required.");
            return Error.Validation("Club.Validation", "Club name is required.");
        }

        if (newClub.Name.Length > 100)
        {
            _logger.LogError("Club name cannot exceed 100 characters.");
            return Error.Validation("Club.Validation", "Club name cannot exceed 100 characters.");
        }

        if (string.IsNullOrWhiteSpace(newClub.Bio))
        {
            _logger.LogError("Club bio is required.");
            return Error.Validation("Club.Validation", "Club description is required.");
        }

        if (newClub.Bio.Length > 500)
        {
            _logger.LogError("Club bio cannot exceed 500 characters.");
            return Error.Validation("Club.Validation", "Club description cannot exceed 500 characters.");
        }

        if (!string.IsNullOrWhiteSpace(newClub.PaymentDetails))
        {
            if (newClub.PaymentDetails.Length > 200)
            {
                _logger.LogError("Payment details cannot exceed 200 characters.");
                return Error.Validation("Club.Validation", "Payment details cannot exceed 200 characters.");
            }

            if (!IsValidEmail(newClub.PaymentDetails))
            {
                _logger.LogError("Payment details must be a valid email address.");
                return Error.Validation("Club.Validation", "Payment details must be a valid email address.");
            }
        }

        return ResultT<bool>.Success(true);
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Result> UpdateClubAsync(Club updatedClub)
    {
        var club = await _clubRepository.GetByIdAsync(updatedClub.Id);

        if (club == null)
        {
            _logger.LogError("Club with ID {ClubId} not found.", updatedClub.Id);
            return ClubServiceError.UpdateFailure;
        }

        if (updatedClub.PaymentDetails != null)
            club.PaymentDetails = updatedClub.PaymentDetails;

        if (updatedClub.Name != null)
            club.Name = updatedClub.Name;

        if (updatedClub.Bio != null)
            club.Bio = updatedClub.Bio;

        if (updatedClub.Logo != null)
            club.Logo = updatedClub.Logo;

        var isSuccess = await _clubRepository.UpdateAsync(club);

        if (!isSuccess)
        {
            _logger.LogError("Failed to update club with ID {ClubId}", updatedClub.Id);
            return ClubServiceError.UpdateFailure;
        }

        return Result.Success();
    }

    public async Task<Result> DeleteClubAsync(int id)
    {
        var result = await _clubRepository.DeleteAsync(id);

        if (!result)
        {
            _logger.LogError("Failed to delete club with ID {ClubId}", id);
            return ClubServiceError.DeleteFailure;
        }

        return Result.Success();
    }

    public async Task<ResultT<ClubAdmin>> AddClubAdminAsync(ClubAdmin newClubAdmin)
    {
        ClubAdmin clubAdmin = new()
        {
            UserId = newClubAdmin.UserId,
            ClubId = newClubAdmin.ClubId,
        };

        var savedClubAdmin = await _adminRepository.AddAsync(clubAdmin);

        if (savedClubAdmin == null)
        {
            _logger.LogError("Failed to add club admin for user {UserId} and club {ClubId}", newClubAdmin.UserId, newClubAdmin.ClubId);
            return ClubServiceError.CreateFailure;
        }

        return ResultT<ClubAdmin>.Success(savedClubAdmin);
    }
}

