using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Boardify.Api.Controllers.Base
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TrelloControllerBase : ControllerBase
    {
        protected int CreatorUserId => Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

        protected string AccessToken
        {
            get
            {
                var authHeader = Request.Headers.Authorization.ToString();
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    return authHeader.Substring("Bearer ".Length).Trim();
                }
                return string.Empty;
            }
        }
    }
}