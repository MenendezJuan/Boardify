using AutoMapper;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Features.Boards.Models.GetBoardWithColumnsAndCardsAsync;
using Boardify.Application.Features.Boards.Models.Testing;
using Boardify.Application.Features.Users.Models;
using Boardify.Application.Features.Workspaces.Models;
using Boardify.Domain.Entities;
using Boardify.Domain.Enums;
using Boardify.Domain.Relationships;

namespace Boardify.Application.MappingProfiles
{
    public class BoardProfile : Profile
    {
        public BoardProfile()
        {
            CreateMap<Board, CreateBoardResponseModel>();

            CreateMap<CreateBoardModel, Board>();

            CreateMap<UpdateBoardModel, Board>()
                .ReverseMap();

            CreateMap<UpdateBoardResponseModel, Board>()
                .ReverseMap();


            CreateMap<Board, BoardResponseDto>()
               .ForMember(dest => dest.CompleteBoard, opt => opt.MapFrom(src => new CompleteBoardDto
               {
                   Columns = src.Columns.OrderBy(c => c.Position).ToDictionary(c => c.Id, c => new ColumnDto
                   {
                       Id = c.Id,
                       Name = c.Name,
                       Position = c.Position,
                       CardIds = c.Cards.OrderBy(card => card.Id).Select(card => card.Id).ToList()
                   }),
                   ColumnOrder = src.Columns.OrderBy(c => c.Position).Select(c => c.Id).ToList(),
                   Cards = src.Columns.SelectMany(c => c.Cards).ToDictionary(card => card.Id, card => new CardDto
                   {
                       Id = card.Id,
                       Name = card.Name,
                       ColumnId = card.ColumnId,
                       DueDate = card.DueDate == DateTime.MinValue ? (DateTime?)null : card.DueDate,
                       StartDate = card.StartDate == DateTime.MinValue ? (DateTime?)null : card.StartDate,
                       Priority = card.Priority,
                       PriorityName = card.Priority.ToString(),
                       Assignees = card.CardMembers.Select(cm => new UserCardMemberModel
                       {
                           Id = cm.User.Id,
                           Name = cm.User.Name,
                           LastName = cm.User.LastName,
                           Avatar = cm.User.Avatar
                       }).ToList(),
                       CommentCount = 0,
                       AttachmentCount = 0,
                       AttachmentPreview = null,
                       Labels = card.CardLabels.Select(cl => cl.LabelId).ToList()
                   })
               }))
               .ForMember(dest => dest.MemberCount, opt => opt.MapFrom(src => src.BoardMembers.Count))
               .ForMember(dest => dest.IdVisibility, opt => opt.MapFrom(src => (VisibilityEnum)src.Visibility))
               .ReverseMap();

            CreateMap<Board, BoardInfoModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.WorkspaceId, opt => opt.MapFrom(src => src.WorkspaceId));

            CreateMap<BoardLabel, AddLabelToBoardResponseModel>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                   .ForMember(dest => dest.Colour, opt => opt.MapFrom(src => src.Colour));

            CreateMap<AddLabelToBoardModel, BoardLabel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Colour, opt => opt.MapFrom(src => src.Colour));

            CreateMap<UpdateBoardLabelModel, BoardLabel>()
                .ReverseMap();

            CreateMap<UpdateBoardLabelResponseModel, BoardLabel>()
                .ReverseMap();

            CreateMap<RemoveLabelFromBoardModel, BoardLabel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BoardLabelId))
                .ReverseMap();

            CreateMap<BoardLabelModel, BoardLabel>()
               .ReverseMap();
        }
    }
}