﻿namespace Boardify.Application.Features.Users.Models
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
    }
}