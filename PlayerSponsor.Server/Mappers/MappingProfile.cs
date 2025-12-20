using AutoMapper;
using PlayerSponsor.Server.Controllers.Requests;
using PlayerSponsor.Server.Models;
using PlayerSponsor.Server.Services.Commands;
using PlayerSponsor.Server.Services.DTOs;

namespace PlayerSponsor.Server.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Club, ClubDto>();
        CreateMap<CreateClubRequest, CreateClubCommand>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ClubDetails.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ClubDetails.Description))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ClubDetails.Email))
            .ForMember(dest => dest.PrimaryColour, opt => opt.MapFrom(src => src.ClubDetails.PrimaryColour))
            .ForMember(dest => dest.SecondaryColour, opt => opt.MapFrom(src => src.ClubDetails.SecondaryColour))
            .ForMember(dest => dest.HeroImageId, opt => opt.MapFrom(src => src.ClubDetails.HeroImageId))
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.ClubDetails.Slug));

        CreateMap<CreateClubRequest, CreateUserCommand>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AdminAccountDetails.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.AdminAccountDetails.Password))
            .ForMember(dest => dest.ConfirmPassword, opt => opt.MapFrom(src => src.AdminAccountDetails.ConfirmPassword));
        CreateMap<CreateClubCommand, Club>();
        CreateMap<UpdateClubDetailsRequest, UpdateClubDetailsCommand>();

    }
}

