using AutoMapper;
using Boardify.Application.Features.Boards.Models.GetBoardWithColumnsAndCardsAsync;
using Boardify.Application.Features.Columns.Models;
using Boardify.Domain.Entities;

namespace Boardify.Application.MappingProfiles
{
    public class ColumnProfile : Profile
    {
        public ColumnProfile()
        {
            CreateMap<CreateColumnModel, Column>()
                .ReverseMap();
            CreateMap<Column, CreateColumnResponseModel>()
                .ReverseMap();

            CreateMap<UpdateColumnModel, Column>()
                .ReverseMap();
            CreateMap<Column, UpdateColumnResponseModel>()
                .ReverseMap();
            CreateMap<Column, ColumnResponseModel>()
                .ReverseMap();

            CreateMap<Column, ColumnDto>()
         .ForMember(dest => dest.CardIds, opt => opt.MapFrom(src => src.Cards.Select(card => card.Id).ToList()))
         .ReverseMap();

            CreateMap<Column, UpdateColumnOrderModel>()
                .ReverseMap();
        }
    }
}