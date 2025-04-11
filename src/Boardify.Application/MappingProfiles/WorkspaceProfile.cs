using AutoMapper;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Features.Workspaces.Queries.Models;
using Boardify.Domain.Entities;

namespace Boardify.Application.MappingProfiles
{
    public class WorkspaceProfile : Profile
    {
        public WorkspaceProfile()
        {
            CreateMap<CreateWorkspaceModel, Workspace>().ReverseMap();
            CreateMap<CreateWorkspaceResponseModel, Workspace>().ReverseMap();

            CreateMap<UpdateWorkspaceModel, Workspace>().ReverseMap();
            CreateMap<UpdateWorkspaceResponseModel, Workspace>().ReverseMap();

            CreateMap<GetOwnedWorkspacesModel, Workspace>().ReverseMap();

            CreateMap<Workspace, WorkspaceInfoModel>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Boards, opt => opt.MapFrom(src => src.Boards));
        }
    }
}