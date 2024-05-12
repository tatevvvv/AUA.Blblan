namespace Blblan.WebApi.Services;

using Blblan.Common.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService
{
    private const int ExpirationMinutes = 60;
    private readonly ILogger<TokenService> _logger;
    private readonly JwtTokenSettings _options;

    public TokenService(ILogger<TokenService> logger, IOptions<JwtTokenSettings> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    public string CreateToken(UserResponse user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(
            CreateClaims(user),
            CreateSigningCredentials(),
            expiration
        );
        var tokenHandler = new JwtSecurityTokenHandler();

        _logger.LogInformation("JWT Token created");

        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
        DateTime expiration) =>
        new(
            _options.ValidIssuer,
            _options.ValidAudience,
            claims,
            expires: expiration,
            signingCredentials: credentials
        );

    private List<Claim> CreateClaims(UserResponse user)
    {
        var jwtSub = _options.JwtRegisteredClaimNamesSub;

        try
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwtSub),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                 //new Claim(ClaimTypes.Email, user.Email),
            };

            return claims;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private SigningCredentials CreateSigningCredentials()
    {
        var symmetricSecurityKey = _options.SymmetricSecurityKey;

        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(symmetricSecurityKey)
            ),
            SecurityAlgorithms.HmacSha256
        );
    }
}