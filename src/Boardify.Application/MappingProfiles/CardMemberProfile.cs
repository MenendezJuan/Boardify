using AutoMapper;
using Boardify.Application.Features.Cards.Models;
using Boardify.Domain.Relationships;

namespace Boardify.Application.MappingProfiles
{
    public class CardMemberProfile : Profile
    {
        public CardMemberProfile()
        {
            CreateMap<CardMember, CardMemberResponseModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ReverseMap();

            CreateMap<CardMember, RemoveMemberRequestModel>()
                .ReverseMap();
        }
    }
}