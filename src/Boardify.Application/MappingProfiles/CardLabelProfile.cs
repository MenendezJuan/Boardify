using AutoMapper;
using Boardify.Application.Features.CardLabels.Models;
using Boardify.Domain.Relationships;

namespace Boardify.Application.MappingProfiles
{
    public class CardLabelProfile : Profile
    {
        public CardLabelProfile()
        {
            CreateMap<CardLabel, CardLabelModel>()
              .ForMember(dest => dest.LabelName, opt => opt.MapFrom(src => src.BoardLabel.Name))
              .ForMember(dest => dest.LabelColour, opt => opt.MapFrom(src => src.BoardLabel.Colour));
            CreateMap<CardLabel, CreateCardLabelModel>()
                .ReverseMap();

            CreateMap<CardLabel, UpdateCardLabelModel>()
                .ReverseMap();
        }
    }
}