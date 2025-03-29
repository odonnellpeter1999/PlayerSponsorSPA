using AutoMapper;
using PlayerSponsor.Server.Controllers.Requests;
using PlayerSponsor.Server.Controllers.Responses;
using PlayerSponsor.Server.Models;

namespace PlayerSponsor.Server.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Club, ClubDto>();
        CreateMap<CreateClubRequest, Club>();
        CreateMap<UpdateClubDto, Club>();
    }
}

