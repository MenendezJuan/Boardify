using AutoMapper;
using Boardify.Application.Features.Activity.Models;
using Boardify.Application.Features.Boards.Models.GetBoardWithColumnsAndCardsAsync;
using Boardify.Application.Features.Cards.Models;
using Boardify.Domain.Entities;
using Boardify.Domain.Enums;
using Boardify.Domain.Relationships;

namespace Boardify.Application.MappingProfiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<CreateCardModel, Card>()
                .ReverseMap();
            CreateMap<Card, CreateCardResponseModel>()
                .ForMember(dest => dest.PriorityName, opt => opt.MapFrom(src => src.Priority.ToString()))
                .ReverseMap();

            CreateMap<UpdateCardModel, Card>()
               .ReverseMap();

            CreateMap<Card, UpdateCardResponseModel>()
                .ReverseMap();

            CreateMap<Card, CardResponseModel>()
                .ReverseMap();

            CreateMap<Card, CardDto>()
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.PriorityName, opt => opt.MapFrom(src => src.Priority.ToString()))
            .ForMember(dest => dest.PriorityName, opt => opt.MapFrom(src => src.Priority.ToString()))
            .ForMember(dest => dest.Assignees, opt => opt.MapFrom(src => src.CardMembers.Select(cm => cm.User)))
            .ReverseMap();

            CreateMap<Card, UpdateCardOrderModel>().ReverseMap();
            CreateMap<Card, UpdateCardOrderResponseModel>().ReverseMap();

            CreateMap<Card, MoveCardRequestModel>().ReverseMap();

            CreateMap<Card, BoardCardDetailResponse>()
      .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.CardLabels.Select(cl => cl.BoardLabel)))
      .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.CardAttachments))
      .ForMember(dest => dest.Assignee, opt => opt.MapFrom(src => src.CardMembers.Select(cm => cm.User)))
      .ForMember(dest => dest.Reporter, opt => opt.MapFrom(src => src.Reporter))
      .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate == DateTime.MinValue ? (DateTime?)null : src.StartDate))
      .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate == DateTime.MinValue ? (DateTime?)null : src.DueDate))
      .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
      .ForMember(dest => dest.PriorityName, opt => opt.MapFrom(src => src.Priority.ToString()))
      .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
      .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.CardComments))
      .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.CardActivities));

            CreateMap<UpdateCardDatesModel, Card>();

            CreateMap<Card, CardResponseModel>();

            CreateMap<UpdateCardPriorityModel, Card>()
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => (PriorityEnum)src.Priority));

            CreateMap<UpdateCardReporterModel, Card>()
                .ForMember(dest => dest.ReporterId, opt => opt.MapFrom(src => src.ReporterId));

            CreateMap<UpdateCardDescriptionModel, Card>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<UpdateCardDatesModel, Card>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.HasValue ? src.StartDate.Value : (DateTime?)null))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate.HasValue ? src.DueDate.Value : (DateTime?)null));

            CreateMap<Card, UpdateCardReporterResponseModel>()
                .ReverseMap();

            CreateMap<Card, UpdateCardDatesResponseModel>()
                .ReverseMap();

            CreateMap<Card, UpdateCardDescriptionResponseModel>()
                .ReverseMap();

            CreateMap<Card, UpdateCardPriorityResponseModel>()
                .ReverseMap();

            CreateMap<CardActivity, CardActivityResponseModel>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
           .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Activity ?? string.Empty))
           .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType))
           .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationTime));

        }
    }
}