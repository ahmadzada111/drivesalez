using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DriveSalez.Core.Services;

public class PaymentService : IPaymentService
{
    private readonly IAccountService _accountService;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public PaymentService(IPaymentRepository paymentRepository,
        IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager, IAccountService accountService)
    {
        _paymentRepository = paymentRepository;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
        _accountService = accountService;
    }

    public async Task<bool> TopUpBalance(PaymentRequestDto request)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not Authorized");
        }

        if (request.Sum <= 0)
        {
            return false;
        }
        
        var result = await _paymentRepository.RecordBalanceTopUpInDbAsync(user.Id, request);

        if (!result)
        {
            return false;
        }
        
        return true;
    }

    public async Task<bool> AddPremiumAnnouncementLimit(int announcementQuantity, int subscriptionId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not Authorized");
        }

        if (announcementQuantity <= 0)
        {
            return false;
        }
        
        var result = await _paymentRepository.AddPremiumAnnouncementLimitInDbAsync(user.Id, announcementQuantity, subscriptionId);

        if (!result)
        {
            return false;
        }
        
        return true;
    }

    public async Task<bool> AddRegularAnnouncementLimit(int announcementQuantity, int subscriptionId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not Authorized");
        }

        if (announcementQuantity <= 0)
        {
            return false;
        }
        
        var result = await _paymentRepository.AddRegularAnnouncementLimitInDbAsync(user.Id, announcementQuantity, subscriptionId);

        if (!result)
        {
            return false;
        }
        
        return true;
    }
    
    public async Task<bool> BuyPremiumAccount(int subscriptionId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
        
        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not Authorized");
        }

        var subscription = await _paymentRepository.GetSubscriptionFromDbAsync(subscriptionId);
        
        if (user.AccountBalance - subscription.Price.Price > 0)
        {
            var premiumAccount = await _accountService.ChangeUserTypeToPremiumAccountAsync(user);
            
            if (premiumAccount == null)
            {
                return false;
            }

            return true;
        }
        
        return false;
    }
    
    public async Task<bool> BuyBusinessAccount(int subscriptionId)
    {
        var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not Authorized");
        }

        var subscription = await _paymentRepository.GetSubscriptionFromDbAsync(subscriptionId); 
        
        if (user.AccountBalance - subscription.Price.Price > 0)
        {
            var businessAccount = await _accountService.ChangeUserTypeToBusinessAccountAsync(user);
            
            if (businessAccount == null)
            {
                return false;
            }

            return true;
        }
        
        return false;
    }
}