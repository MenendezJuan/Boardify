using Boardify.Application.DTOs;
using Boardify.Application.Interfaces;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Boardify.External.Servicios.JWTHandler
{
    public class JWTTokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;
        private readonly IQueryRepository<User> _userQueryRepository;

        public JWTTokenHandler(IConfiguration configuration, IQueryRepository<User> userQueryRepository)
        {
            this.configuration = configuration;
            this._userQueryRepository = userQueryRepository;
        }

        public TokenDto CreateAccessToken(User user)
        {
            var keyString = configuration.GetValue<string>("JwtSettings:Key");
            if (string.IsNullOrEmpty(keyString))
            {
                throw new ArgumentNullException(nameof(keyString), "JWT key is not properly configured in the appsettings.json file.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Audience = configuration.GetValue<string>("JwtSettings:Audience"),
                Issuer = configuration.GetValue<string>("JwtSettings:Issuer"),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesciptor);

            Log.Information("Access token created successfully for user {UserId}.", user.Id);

            return new TokenDto
            {
                AccessToken = tokenHandler.WriteToken(token),
                Expiration = token.ValidTo
            };
        }

        public string CreateRefreshToken(User user)
        {
            var keyString = configuration.GetValue<string>("JwtSettings:Key") ?? "default_key"; 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("JwtSettings:RefreshTokenTTL")),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Issuer = configuration.GetValue<string>("JwtSettings:Issuer"),
                Audience = configuration.GetValue<string>("JwtSettings:Audience")
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            Log.Information("Refresh token created successfully for user {UserId}.", user.Id);

            return tokenHandler.WriteToken(token);
        }

        public async Task<TokenDto> RefreshAccessToken(string refreshToken)
        {
            var keyString = configuration.GetValue<string>("JwtSettings:Key");
            var issuer = configuration.GetValue<string>("JwtSettings:Issuer");
            var audience = configuration.GetValue<string>("JwtSettings:Audience");

            if (string.IsNullOrEmpty(keyString) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new Exception("Configuración JWT incompleta.");
            }

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString)),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidIssuers = new[] { issuer },
                ValidateAudience = false,
                ValidAudience = audience,
                ValidAudiences = new[] { audience },
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal claimsPrincipal;
            try
            {
                Log.Information("Trying to validate Refresh Token...");
                claimsPrincipal = tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
                Log.Information("Refresh Token validated.");
            }
            catch (Exception ex)
            {
                Log.Error($"Token validation failed: {ex.Message}");
                throw new Exception("Error al validar el token de refresco.", ex);
            }

            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                Log.Error("Could not extract the user idntifier from the Refresh Token");
                throw new Exception("No se pudo extraer el identificador de usuario del token de refresco.");
            }
            var expClaim = claimsPrincipal.FindFirst("exp")?.Value;
            if (expClaim == null || !long.TryParse(expClaim, out long expUnixTime))
            {
                Log.Error("The refresh token is not valid for its expiration time (exp).");
                throw new Exception("El token de refresco no contiene un valor válido para expiración (exp).");
            }

            var expDateTime = DateTimeOffset.FromUnixTimeSeconds(expUnixTime).UtcDateTime;
            if (expDateTime < DateTime.UtcNow)
            {
                Log.Error("The refresh token is expired");
                throw new Exception("El token de refresco ha expirado.");
            }
            var user = await _userQueryRepository.GetByIdAsync(int.Parse(userId));
            if (user == null)
            {
                Log.Error("Could not find any user associated with the identifier given.");
                throw new Exception("No se encontró ningún usuario asociado al identificador proporcionado.");
            }

            var newAccessToken = CreateAccessToken(user);
            var newRefreshToken = CreateRefreshToken(user);

            return new TokenDto
            {
                AccessToken = newAccessToken.AccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public string GetUserIdFromAccessToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyString = configuration.GetValue<string>("JwtSettings:Key") ?? "default_jwt_key";
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString)),
                ValidateIssuer = true,
                ValidIssuer = configuration.GetValue<string>("JwtSettings:Issuer"),
                ValidateAudience = false,
                ValidAudience = configuration.GetValue<string>("JwtSettings:Audience"),
                ValidateLifetime = true
            };

            try
            {
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(accessToken, validationParameters, out SecurityToken validatedToken);
                var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    Log.Error("Could not find user Id in the token");
                    throw new Exception("No se encontró el ID del usuario en el token.");
                }

                return userIdClaim.Value;
            }
            catch (Exception ex)
            {
                Log.Error("Error decoding the access token", ex);
                throw new Exception("Error al decodificar el token de acceso.", ex);
            }
        }
    }
}