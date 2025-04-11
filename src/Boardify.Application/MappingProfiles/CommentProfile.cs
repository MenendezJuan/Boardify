using AutoMapper;
using Boardify.Application.Features.Comments.Models;
using Boardify.Domain.Relationships;

namespace Boardify.Application.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<AddCommentModel, CardComment>();

            CreateMap<CardComment, AddCommentResponseModel>();

            CreateMap<UpdateCommentModel, CardComment>()
            .ForMember(dest => dest.CreatedTime, opt => opt.Ignore());
            CreateMap<CardComment, UpdateCommentResponseModel>();
        }
    }
}
