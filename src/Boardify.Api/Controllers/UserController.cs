using Boardify.Api.Controllers.Base;
using Boardify.Application.DTOs;
using Boardify.Application.Features.Users.Models;
using Boardify.Application.Features.Users.Queries.Models;
using Boardify.Application.Interfaces.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boardify.Api.Controllers
{
    public class UserController : TrelloControllerBase
    {
        private readonly IUserBusinessLogic _userBusinessLogic;

        public UserController(IUserBusinessLogic userBusinessLogic)
        {
            _userBusinessLogic = userBusinessLogic ?? throw new ArgumentNullException(nameof(userBusinessLogic));
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>The created user.</returns>
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserModel userModel)
        {
            var result = await _userBusinessLogic.CreateUser(userModel);

            if (result.IsSuccess())
            {
                var createdUser = result.SuccessValue;
                return CreatedAtAction(nameof(GetByEmail), new { email = createdUser.Email }, createdUser);
            }
            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A confirmation of deletion.</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userBusinessLogic.DeleteUser(id);
            return result.IsSuccess() ? NoContent() : NotFound(new {result.Errors, result.Message});
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>The updated user.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserModel userModel)
        {
            var result = await _userBusinessLogic.UpdateUser(AccessToken, userModel);

            if (result.IsSuccess())
            {
                var updatedUser = result.SuccessValue;
                return Ok(updatedUser);
            }

            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates the password for the current user.
        /// </summary>
        /// <param name="model">The password update model.</param>
        /// <returns>The result of the password update operation.</returns>
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordModel model)
        {
            var result = await _userBusinessLogic.UpdatePassword(AccessToken, model);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }

            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The user.</returns>
        [HttpGet("get-by-id/{userId}")]
        public async Task<IActionResult> GetById(int userId)
        {
            var result = await _userBusinessLogic.GetUserById(userId);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }

            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Gets a user by email.
        /// </summary>
        /// <param name="email">The user email.</param>
        /// <returns>The user.</returns>
        [HttpGet("get-by-email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await _userBusinessLogic.GetUserByEmailAddress(email);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }

            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Refreshes the access token using the provided refresh token.
        /// </summary>
        /// <param name="request">The token revalidation request.</param>
        /// <returns>The result of the token revalidation operation.</returns>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRevalidationRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.RefreshToken))
            {
                return BadRequest("Refresh token not provided.");
            }

            var result = await _userBusinessLogic.RevalidateToken(request.RefreshToken);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }

            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="loginUser">The login model.</param>
        /// <returns>The authentication result.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginUser)
        {
            var result = await _userBusinessLogic.Login(loginUser);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }

            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Gets the workspaces associated with the current user.
        /// </summary>
        /// <returns>The user's workspaces.</returns>
        [HttpGet("workspaces")]
        public async Task<IActionResult> GetUserWorkspaces()
        {
            var userId = CreatorUserId;
            var result = await _userBusinessLogic.GetUserWorkspacesAsync(userId);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }

            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Searches for users based on the provided search text.
        /// </summary>
        /// <param name="searchText">The text to search for.</param>
        /// <returns>A list of users matching the search criteria.</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] string searchText)
        {
            var result = await _userBusinessLogic.SearchUsersAsync(searchText);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }

            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Gets an image by its relative URL.
        /// </summary>
        /// <param name="relativeUrl">The relative URL of the image.</param>
        /// <returns>The image file.</returns>
        [AllowAnonymous]
        [HttpGet("image")]
        public async Task<IActionResult> GetImage([FromQuery] string relativeUrl)
        {
            var result = await _userBusinessLogic.GetImageResultAsync(relativeUrl);
            return result != null ? Ok(result) : NotFound(new {result.Errors, result.Message});
        }
    }
}