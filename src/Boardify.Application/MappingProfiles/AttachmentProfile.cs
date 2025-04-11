using AutoMapper;
using Boardify.Application.Features.Attachments.Models;
using Boardify.Domain.Relationships;

namespace Boardify.Application.MappingProfiles
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<CardAttachment, AttachmentResponseModel>()
                .ReverseMap();

            CreateMap<CardAttachment, CardAttachmentDetailModel>()
                .ReverseMap();
        }
    }
}