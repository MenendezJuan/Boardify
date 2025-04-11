using AutoMapper;
using Boardify.Application.Features.Boards.Models;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;

namespace Boardify.Application.MappingProfiles
{
    public class BoardMemberProfile : Profile
    {
        public BoardMemberProfile()
        {
            CreateMap<AddMemberToBoardModel, BoardMember>()
                    .ReverseMap();

            CreateMap<BoardMember, BoardMemberModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                    .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.User.Avatar))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName));

            CreateMap<Board, RemoveMemberFromBoardModel>()
                .ReverseMap();
        }
    }
}