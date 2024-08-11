using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Services;

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

    // public async Task<bool> TopUpBalance(PaymentRequestDto request)
    // {
    //     var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
    //
    //     if (user == null)
    //     {
    //         throw new UserNotAuthorizedException("User is not Authorized");
    //     }
    //
    //     if (request.Sum <= 0)
    //     {
    //         return false;
    //     }
    //     
    //     var result = await _paymentRepository.RecordBalanceTopUpInDbAsync(user, request);
    //
    //     if (!result)
    //     {
    //         return false;
    //     }
    //     
    //     return true;
    // }

    public async Task<bool> AddAnnouncementLimit(int announcementQuantity, int subscriptionId)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not Authorized");
        }

        var result = await _paymentRepository.AddAnnouncementLimitInDbAsync(user.Id, announcementQuantity, subscriptionId);

        if (!result)
        {
            return false;
        }
        
        return true;
    }
    
    // public async Task<bool> AddPremiumAnnouncementLimit(int announcementQuantity, int subscriptionId)
    // {
    //     var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
    //
    //     if (user == null)
    //     {
    //         throw new UserNotAuthorizedException("User is not Authorized");
    //     }
    //
    //     if (announcementQuantity <= 0)
    //     {
    //         return false;
    //     }
    //     
    //     var result = await _paymentRepository.AddPremiumAnnouncementLimitInDbAsync(user, announcementQuantity, subscriptionId);
    //
    //     if (!result)
    //     {
    //         return false;
    //     }
    //     
    //     return true;
    // }
    //
    // public async Task<bool> AddRegularAnnouncementLimit(int announcementQuantity, int subscriptionId)
    // {
    //     var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext?.User);
    //
    //     if (user == null)
    //     {
    //         throw new UserNotAuthorizedException("User is not Authorized");
    //     }
    //
    //     if (announcementQuantity <= 0)
    //     {
    //         return false;
    //     }
    //     
    //     var result = await _paymentRepository.AddRegularAnnouncementLimitInDbAsync(user, announcementQuantity, subscriptionId);
    //
    //     if (!result)
    //     {
    //         return false;
    //     }
    //     
    //     return true;
    // }
    
    public async Task<bool> BuyBusinessAccount(int subscriptionId)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not Authorized");
        var subscription = await _paymentRepository.GetSubscriptionFromDbAsync(subscriptionId); 
        
        if (user.AccountBalance - subscription?.Price > 0)
        {
            await _accountService.ChangeUserTypeToBusinessAccountAsync(user);

            return true;
        }
        
        return false;
    }
}