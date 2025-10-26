using AutoMapper;
using PlayerSponsor.Server.Controllers.Requests;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Services.DTOs;

namespace PlayerSponsor.Server.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Club, ClubDto>();
        CreateMap<CreateClubRequest, Club>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ClubDetails.Name))
            .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.ClubDetails.Description))
            .ForMember(dest => dest.PaymentDetails, opt => opt.MapFrom(src => src.ClubDetails.InteracEmail));
        CreateMap<CreateClubRequest, NewApplicationUser>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AdminAccountDetails.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.AdminAccountDetails.Password))
            .ForMember(dest => dest.ConfirmPassword, opt => opt.MapFrom(src => src.AdminAccountDetails.ConfirmPassword));
        CreateMap<UpdateClubRequest, Club>();
    }
}

