using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using Azure;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Entities;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Core.ServiceContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IJwtService _jwtService;
    private readonly IOtpService _otpService;
    private readonly IEmailService _emailService;

    public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
        RoleManager<ApplicationRole> roleManager, IJwtService jwtService, IOtpService otpService, IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _jwtService = jwtService;
        _otpService = otpService;
        _emailService = emailService;
    }
    
    public async Task<AuthenticationResponseDto> SendRegistrationDataToDb(ApplicationUser user, string password)
    {
        user.EmailConfirmed = false;
        
        IdentityResult result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            if (await _roleManager.FindByNameAsync(UserType.DefaultAccount.ToString()) == null)
            {
                ApplicationRole applicationRole = new ApplicationRole()
                {
                    Name = UserType.DefaultAccount.ToString()
                };

                await _roleManager.CreateAsync(applicationRole);
                await _userManager.AddToRoleAsync(user, UserType.DefaultAccount.ToString());
            }
            else
            {
                await _userManager.AddToRoleAsync(user, UserType.DefaultAccount.ToString());
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            var response = await _jwtService.GenerateSecurityToken(user);
            user.RefreshToken = response.RefreshToken;
            user.RefreshTokenExpiration = response.RefreshTokenExpiration;
            await _userManager.UpdateAsync(user);

            return response;
        }

        return new AuthenticationResponseDto{Error = string.Join(" | ", result.Errors.Select(e => e.Description))};
    }
    
    public async Task<AuthenticationResponseDto> SendLoginDataToDb(LoginDto request)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return null;
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            var response = await _jwtService.GenerateSecurityToken(user);
            user.RefreshToken = response.RefreshToken;
            user.RefreshTokenExpiration = response.RefreshTokenExpiration;
            await _userManager.UpdateAsync(user);

            return response;
        }
        else
        {
            return null;
        }
    }

    public async Task<AuthenticationResponseDto> SendRefreshTokenDataToDb(RefreshJwtDto request)
    {
        ClaimsPrincipal principal = _jwtService.GetPrincipalFromJwtToken(request.Token);

        if (principal == null)
        {
            return new AuthenticationResponseDto { Error = "Invalid JWT token"};
        }

        string email = principal.FindFirstValue(ClaimTypes.Email);
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);

        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now)
        {
            return new AuthenticationResponseDto { Error = "Invalid refresh token" };
        }

        var response = await _jwtService.GenerateSecurityToken(user);
        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiration = response.RefreshTokenExpiration;

        await _userManager.UpdateAsync(user);

        return response;
    }
}