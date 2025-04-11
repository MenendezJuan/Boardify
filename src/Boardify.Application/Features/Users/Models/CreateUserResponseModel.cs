namespace Boardify.Application.Features.Users.Models
{
    public class CreateUserResponseModel
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}