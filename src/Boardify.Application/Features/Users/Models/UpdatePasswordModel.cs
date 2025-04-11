namespace Boardify.Application.Features.Users.Models
{
    public class UpdatePasswordModel
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}