using AutoMapper;
using Boardify.Application.Features.ChecklistItems.Models;
using Boardify.Domain.Relationships;

namespace Boardify.Application.MappingProfiles
{
    public class ChecklistItemProfile : Profile
    {
        public ChecklistItemProfile()
        {
            CreateMap<ChecklistItem, ChecklistItemResponse>();
            CreateMap<CreateChecklistItemRequest, ChecklistItem>()
                .ForMember(dest => dest.IsChecked, opt => opt.MapFrom(src => false));

            CreateMap<UpdateChecklistItemRequest, ChecklistItem>().ReverseMap();
            CreateMap<ChecklistItem, UpdateChecklistItemResponse>();
        }
    }
}