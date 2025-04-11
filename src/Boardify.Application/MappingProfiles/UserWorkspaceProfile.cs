using AutoMapper;
using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Features.Workspaces.Queries.Models;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;

namespace Boardify.Application.MappingProfiles
{
    public class UserWorkspaceProfile : Profile
    {
        public UserWorkspaceProfile()
        {
            CreateMap<AddMemberToWorkspaceModel, UserWorkspace>().ReverseMap();
            CreateMap<AddMemberToWorkspaceResponseModel, UserWorkspace>().ReverseMap();

            CreateMap<UserWorkspace, WorkspaceMemberModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<UserWorkspace, UserWorkspacesModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WorkspaceId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Workspace.Name))
                .ReverseMap();

            CreateMap<Workspace, UserWorkspacesModel>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<User, WorkspaceMemberModel>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
              .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
              .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
              .ReverseMap();
        }
    }
}