using System.Security.Claims;
using DriveSalez.Core.Domain.Entities;
using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.Domain.RepositoryContracts;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.Core.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IJwtService _jwtService;
    private readonly IAccountRepository _accountRepository;
    private readonly IFileService _fileService;
    
    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
        RoleManager<ApplicationRole> roleManager, IJwtService jwtService, IHttpContextAccessor contextAccessor,
        IAccountRepository accountRepository, IFileService fileService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _contextAccessor = contextAccessor;
        _jwtService = jwtService;
        _accountRepository = accountRepository;
        _fileService = fileService;
    }

    public async Task<IdentityResult> RegisterDefaultAccountAsync(RegisterDefaultAccountDto request)
    {
        DefaultAccount user = new DefaultAccount()
        {
            Email = request.Email,
            PhoneNumbers = request.PhoneNumbers,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailConfirmed = false,
            CreationDate = DateTimeOffset.Now,
            LastUpdateDate = DateTimeOffset.Now,
            SubscriptionExpirationDate = DateTimeOffset.Now.AddMonths(1),
            IsBanned = false
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

            await _accountRepository.AddLimitsToAccountInDbAsync(user, UserType.DefaultAccount);
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
            UserName = request.UserName,
            WorkHours = request.WorkHours,
            Address = request.Address,
            Description = request.Description,
            EmailConfirmed = false,
            CreationDate = DateTimeOffset.Now,
            LastUpdateDate = DateTimeOffset.Now,
            SubscriptionExpirationDate = DateTimeOffset.Now,
            IsBanned = false
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
            
            await _accountRepository.AddLimitsToAccountInDbAsync(user, UserType.PremiumAccount);
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
            WorkHours = request.WorkHours,
            UserName = request.UserName,
            Address = request.Address,
            Description = request.Description,
            IsOfficial = false,
            EmailConfirmed = false,
            CreationDate = DateTimeOffset.Now,
            LastUpdateDate = DateTimeOffset.Now,
            SubscriptionExpirationDate = DateTimeOffset.Now,
            IsBanned = false
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

            await _accountRepository.AddLimitsToAccountInDbAsync(user, UserType.BusinessAccount);
            return result;
        }

        return result;
    }
    
    public async Task<AuthenticationResponseDto?> LoginAsync(LoginDto request)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await _accountRepository.FindUserByLoginInDbAsync(request.UserName);

            if (user == null)
            {
                throw new UserNotFoundException("User with provided login wasn't found!");
            }

            if (!user.EmailConfirmed)
            {
                throw new EmailNotConfirmedException("Email not confirmed!");
            }

            if (user.IsBanned)
            {
                throw new BannedUserException("User is banned!");
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

    public async Task<AuthenticationResponseDto?> LoginStaffAsync(LoginDto request)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await _accountRepository.FindUserByLoginInDbAsync(request.UserName);
            
            if (user == null)
            {
                throw new UserNotFoundException("User with provided login wasn't found!");
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
        var user = await _userManager.FindByEmailAsync(email);

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
            throw new UserNotFoundException("User with provided email wasn't found!");
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
            throw new UserNotFoundException("User with provided email wasn't found!");
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

    public async Task<bool> ChangeEmailAsync(string email, string newMail)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null)
        {
            throw new UserNotFoundException("User with provided email wasn't found!");
        }

        user.Email = newMail;
        user.UserName = newMail;
        
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return true;
        }
        
        return false;
    }
    
    public async Task LogOutAsync()
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User not authorized!");
        }

        await _signInManager.SignOutAsync();
    }
    
    public async Task<ApplicationUser> DeleteUserAsync(string password)
    {
        var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
        var user = await _userManager.Users
            .Include(u => u.PhoneNumbers)
            .FirstOrDefaultAsync(u => u.Id.ToString() == currentUser.Id.ToString());

        if (user != null  && await _userManager.CheckPasswordAsync(user, password))
        {
            var result = await _accountRepository.DeleteUserFromDbAsync(user);
            
            await _fileService.DeleteAllFilesAsync(user.Id);
            return result;
        }

        throw new UserNotAuthorizedException("User not authorized!");
    }

    public async Task<ApplicationUser> ChangeUserTypeToDefaultAccountAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);

        var defaultAccount = await _accountRepository.ChangeUserTypeToDefaultAccountInDbAsync(user);
        
        await _userManager.AddToRoleAsync(defaultAccount, UserType.DefaultAccount.ToString());
        await _userManager.UpdateAsync(defaultAccount);

        return defaultAccount;
    }
    
    public async Task<ApplicationUser> ChangeUserTypeToPremiumAccountAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);

        var premiumAccount = await _accountRepository.ChangeUserTypeToPremiumInDbAsync(user);
        
        await _userManager.AddToRoleAsync(premiumAccount, UserType.PremiumAccount.ToString());
        await _userManager.UpdateAsync(premiumAccount);

        return premiumAccount;
    }
    
    public async Task<ApplicationUser> ChangeUserTypeToBusinessAccountAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);

        var businessAccount = await _accountRepository.ChangeUserTypeToBusinessInDbAsync(user);
        
        await _userManager.AddToRoleAsync(businessAccount, UserType.BusinessAccount.ToString());
        await _userManager.UpdateAsync(businessAccount);
        
        return businessAccount;
    }
    
    public async Task<IdentityResult> CreateAdminAsync()
    {
        DefaultAccount user = new DefaultAccount()
        {
            Email = "admin",
            UserName = "admin",
            FirstName = "admin" ,
            LastName = "admin",
            EmailConfirmed = true
        };

        IdentityResult result = await _userManager.CreateAsync(user, "Admin1234!");

        if (result.Succeeded)
        {
            if (await _roleManager.FindByNameAsync(UserType.Admin.ToString()) == null)
            {
                ApplicationRole applicationRole = new ApplicationRole()
                {
                    Name = UserType.Admin.ToString()
                };

                await _roleManager.CreateAsync(applicationRole);
                await _userManager.AddToRoleAsync(user, UserType.Admin.ToString());
            }
            else
            {
                await _userManager.AddToRoleAsync(user, UserType.Admin.ToString());
            }
            
            return result;
        }

        return result;
    }
}