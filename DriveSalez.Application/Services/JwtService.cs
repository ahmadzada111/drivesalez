using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.UserDTO;
using DriveSalez.SharedKernel.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DriveSalez.Application.Services;

internal sealed class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly RefreshTokenSettings _refreshTokenSettings;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    
    public JwtService(IOptions<JwtSettings> jwtSettings, IOptions<RefreshTokenSettings> refreshTokenSettings, 
        UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _refreshTokenSettings = refreshTokenSettings.Value;
    }

    private async Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser identityUser)
    {
        DateTime expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.Expiration);
        var role = await _userManager.GetRolesAsync(identityUser);
        
        Claim[] claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.NameIdentifier, identityUser.Id.ToString()),
            new Claim(ClaimTypes.Email, identityUser.Email!), 
            new Claim(ClaimTypes.Role, role.FirstOrDefault()!)
        };
        
        var possibleClaims = await _userManager.GetClaimsAsync(identityUser);
        await _userManager.RemoveClaimsAsync(identityUser, possibleClaims);
        await _userManager.AddClaimsAsync(identityUser, claims);
        
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: expiration,
            signingCredentials: signingCredentials
        );

        return token;
    }
    
    public async Task<AuthResponseDto> GenerateSecurityTokenAsync(ApplicationUser identityUser)
    {
        DateTime expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.Expiration);
        JwtSecurityToken token = await CreateJwtTokenAsync(identityUser);
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        string response = tokenHandler.WriteToken(token);
        var user = await _unitOfWork.Users.Find(x => x.IdentityId == identityUser.Id)
            ?? throw new UserNotFoundException("User not found");
        
        return new AuthResponseDto()
        {
            Id = user.Id,
            Token = response,
            JwtExpiration = expiration,
            RefreshToken = GenerateRefreshToken(),
            RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(_refreshTokenSettings.Expiration),
        };
    }

    public ClaimsPrincipal GetPrincipalFromJwtToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is JwtSecurityToken jwtSecurityToken && 
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, 
                StringComparison.InvariantCultureIgnoreCase))
        {
            return principal;
        }

        throw new SecurityTokenException("Invalid JWT token");
    }

    private static string GenerateRefreshToken()
    {
        byte[] bytes = new byte[64];
        var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(bytes);
        
        return Convert.ToBase64String(bytes);
    }
}
