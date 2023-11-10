using System.Security.Claims;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.Core.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IJwtService _jwtService;
    
    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
        RoleManager<ApplicationRole> roleManager, IJwtService jwtService, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _contextAccessor = contextAccessor;
        _jwtService = jwtService;
    }

    public async Task<IdentityResult> RegisterDefaultAccountAsync(RegisterDefaultAccountDto request)
    {
        DefaultAccount user = new DefaultAccount()
        {
            Email = request.Email,
            PhoneNumber = request.Phone,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailConfirmed = false
        };

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

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

            await _userManager.UpdateAsync(user);

            return result;
        }

        return result;
    }

    public async Task<IdentityResult> RegisterPremiumAccountAsync(RegisterPaidAccountDto request)
    {
        PremiumAccount user = new PremiumAccount()
        {
            Email = request.Email,
            PhoneNumbers = request.PhoneNumbers,
            UserName = request.Email,
            WorkHours = request.WorkHours,
            CompanyName = request.CompanyName,
            Address = request.Address,
            Description = request.Description,
            EmailConfirmed = false
        };

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            if (await _roleManager.FindByNameAsync(UserType.PremiumAccount.ToString()) == null)
            {
                ApplicationRole applicationRole = new ApplicationRole()
                {
                    Name = UserType.PremiumAccount.ToString()
                };

                await _roleManager.CreateAsync(applicationRole);
                await _userManager.AddToRoleAsync(user, UserType.PremiumAccount.ToString());
            }
            else
            {
                await _userManager.AddToRoleAsync(user, UserType.PremiumAccount.ToString());
            }

            await _userManager.UpdateAsync(user);

            return result;
        }

        return result;
    }
    
    public async Task<IdentityResult> RegisterBusinessAccountAsync(RegisterPaidAccountDto request)
    {
        BusinessAccount user = new BusinessAccount()
        {
            Email = request.Email,
            PhoneNumbers = request.PhoneNumbers,
            UserName = request.Email,
            WorkHours = request.WorkHours,
            CompanyName = request.CompanyName,
            Address = request.Address,
            Description = request.Description,
            IsOfficial = false,
            EmailConfirmed = false
        };

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            if (await _roleManager.FindByNameAsync(UserType.BusinessAccount.ToString()) == null)
            {
                ApplicationRole applicationRole = new ApplicationRole()
                {
                    Name = UserType.BusinessAccount.ToString()
                };

                await _roleManager.CreateAsync(applicationRole);
                await _userManager.AddToRoleAsync(user, UserType.BusinessAccount.ToString());
            }
            else
            {
                await _userManager.AddToRoleAsync(user, UserType.BusinessAccount.ToString());
            }

            await _userManager.UpdateAsync(user);

            return result;
        }

        return result;
    }
    
    public async Task<AuthenticationResponseDto> LoginAsync(LoginDto request)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new UserNotFoundException("User not found!");
            }

            if (!user.EmailConfirmed)
            {
                throw new EmailNotConfirmedException("Email is not confirmed!");
            }
            
            await _signInManager.SignInAsync(user, isPersistent: false);
            var response = await _jwtService.GenerateSecurityTokenAsync(user);
            user.RefreshToken = response.RefreshToken;
            user.RefreshTokenExpiration = response.RefreshTokenExpiration;
            await _userManager.UpdateAsync(user);

            return response;
        }
        
        return null;
    }

    public async Task<AuthenticationResponseDto> RefreshAsync(RefreshJwtDto request)
    {
        ClaimsPrincipal principal = _jwtService.GetPrincipalFromJwtToken(request.Token);

        if (principal == null)
        {
            throw new SecurityTokenException("Invalid JWT token");
        }

        string email = principal.FindFirstValue(ClaimTypes.Email);
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);

        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiration <= DateTime.Now)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        var response = await _jwtService.GenerateSecurityTokenAsync(user);
        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiration = response.RefreshTokenExpiration;

        await _userManager.UpdateAsync(user);

        return response;
    }
    
    public async Task<bool> ChangePasswordAsync(ChangePasswordDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user == null)
        {
            throw new UserNotFoundException("User is not found!");
        }

        var passwordValidator = new PasswordValidator<ApplicationUser>();
        var validationResult = await passwordValidator.ValidateAsync(_userManager, user, request.NewPassword);

        if (!validationResult.Succeeded)
        {
            return false;
        }
        
        var changeResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        
        if (changeResult.Succeeded)
        {
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return true;
            }
        }

        return false;
    }
    
    public async Task<bool> ResetPasswordAsync(string email, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null)
        {
            throw new UserNotFoundException("User is not found!");
        }

        var passwordValidator = new PasswordValidator<ApplicationUser>();
        var validationResult = await passwordValidator.ValidateAsync(_userManager, user, newPassword);

        if (!validationResult.Succeeded)
        {
            return false;
        }
        
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
        var result = await _userManager.UpdateAsync(user);
        
        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }
    
    public async Task LogOutAsync()
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not authorized!");
        }

        await _signInManager.SignOutAsync();
    }
    
    public async Task<bool> DeleteUserAsync(string password)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        
        if (user != null  && await _userManager.CheckPasswordAsync(user, password))
        {
            var result = await _userManager.DeleteAsync(user);
            
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        throw new UserNotAuthorizedException("User is not authorized!");
    }
}