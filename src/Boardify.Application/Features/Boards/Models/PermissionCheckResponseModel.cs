using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Boards.Models
{
    public class PermissionCheckResponseModel
    {
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public VisibilityEnum BoardVisibility { get; set; }
    }
}