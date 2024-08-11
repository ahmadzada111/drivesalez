using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using DriveSalez.Application.DTO;
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DriveSalez.Application.Services;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly RefreshTokenSettings _refreshTokenSettings;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    
    public JwtService(IOptions<JwtSettings> jwtSettings, IOptions<RefreshTokenSettings> refreshTokenSettings, 
        UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
        _mapper = mapper;
        _refreshTokenSettings = refreshTokenSettings.Value;
    }

    private async Task<JwtSecurityToken> GenerateSecurityTokenAsync(ApplicationUser user)
    {
        DateTime expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.Expiration);
        var role = await _userManager.GetRolesAsync(user);
        
        Claim[] claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? throw new InvalidOperationException("User email not found")),
            new Claim(ClaimTypes.Role, role.FirstOrDefault()?.ToString() ?? throw new InvalidOperationException("User role not found"))
        };
        
        var possibleClaims = await _userManager.GetClaimsAsync(user);
        await _userManager.RemoveClaimsAsync(user, possibleClaims);
        await _userManager.AddClaimsAsync(user, claims);
        
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
    
    public async Task<DefaultAccountAuthResponseDto> GenerateDefaultAccountSecurityTokenAsync(DefaultAccount user)
    {
        DateTime expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.Expiration);
        JwtSecurityToken token = await GenerateSecurityTokenAsync(user);
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        string response = tokenHandler.WriteToken(token);

        var role = await _userManager.GetRolesAsync(user);

        return new DefaultAccountAuthResponseDto()
        {
            Token = response,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            JwtExpiration = expiration,
            RefreshToken = GenerateRefreshToken(),
            RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(_refreshTokenSettings.Expiration),
            UserRole = role.FirstOrDefault()
        };
    }

    public async Task<BusinessAccountAuthResponseDto> GenerateBusinessAccountSecurityTokenAsync(BusinessAccount user)
    {
        DateTime expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.Expiration);
        JwtSecurityToken token = await GenerateSecurityTokenAsync(user);
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        string response = tokenHandler.WriteToken(token);

        var role = await _userManager.GetRolesAsync(user);

        return new BusinessAccountAuthResponseDto()
        {
            Token = response,
            Email = user.Email,
            PhoneNumbers = _mapper.Map<List<string>>(user.PhoneNumbers),
            JwtExpiration = expiration,
            RefreshToken = GenerateRefreshToken(),
            RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(_refreshTokenSettings.Expiration),
            UserRole = role.FirstOrDefault()
        };
    }


    public ClaimsPrincipal? GetPrincipalFromJwtToken(string token)
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

        try
        {
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is JwtSecurityToken jwtSecurityToken && 
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, 
                    StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }
            
            return null;
        }
        catch
        {
            throw;
        }
    }

    private static string GenerateRefreshToken()
    {
        byte[] bytes = new byte[64];
        var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(bytes);
        
        return Convert.ToBase64String(bytes);
    }
}
