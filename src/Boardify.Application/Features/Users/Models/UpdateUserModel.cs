namespace Boardify.Application.Features.Users.Models
{
    public class UpdateUserModel
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? AvatarBase64 { get; set; }
        public string? AvatarExtension { get; set; }
        public string? AvatarUrl { get; set; }
    }
}