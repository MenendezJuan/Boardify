using AutoMapper;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Features.Users.Models;
using Boardify.Application.Features.Users.Queries.Models;
using Boardify.Application.Features.Workspaces.Models;
using Boardify.Domain.Entities;

namespace Boardify.Application.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserModel, User>().ReverseMap();
            CreateMap<CreateUserResponseModel, User>().ReverseMap();

            CreateMap<UpdateUserModel, User>().ReverseMap();
            CreateMap<User, UpdateUserResponseModel>().ReverseMap();

            CreateMap<GetUserByIdModel, User>().ReverseMap();

            CreateMap<GetUserByEmailAddressModel, User>().ReverseMap();
            CreateMap<UserModel, User>().ReverseMap();

            CreateMap<SearchUserModel, User>().ReverseMap();

            CreateMap<User, UserResponseModel>()
              .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
              .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
              .ReverseMap();

            CreateMap<User, UserCardMemberModel>()
                .ReverseMap();

            CreateMap<BoardMemberModel, User>()
            .ReverseMap();

            CreateMap<User, AddMemberToBoardResponseModel>();

            CreateMap<User, AddMemberToWorkspaceResponseModel>();
        }
    }
}