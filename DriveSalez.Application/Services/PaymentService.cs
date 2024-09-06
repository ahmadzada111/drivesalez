using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Services;

internal sealed class PaymentService : IPaymentService
{
    private readonly IUserService _userService;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public PaymentService(IPaymentRepository paymentRepository,
        IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager, IUserService userService)
    {
        _paymentRepository = paymentRepository;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
        _userService = userService;
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

    public async Task<bool> AddAnnouncementLimitAsync(int announcementQuantity, int subscriptionId)
    {
        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
        var user = await _userManager.GetUserAsync(httpContext.User);

        if (user == null)
        {
            throw new UserNotAuthorizedException("User is not Authorized");
        }

        var result = await _paymentRepository.AddAnnouncementLimitInDbAsync(user.Id, announcementQuantity, subscriptionId);

        return result;
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
    
    // public async Task<bool> BuyBusinessAccountAsync(int subscriptionId)
    // {
    //     var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is null");
    //     var identityUser = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotAuthorizedException("User is not Authorized");
    //     var baseUser = _
    //     var subscription = await _paymentRepository.GetSubscriptionFromDbAsync(subscriptionId);
    //
    //     if (!(identityUser.AccountBalance - subscription?.Price > 0)) return false;
    //     
    //     await _userService.ChangeUserTypeToBusinessAccount(identityUser);
    //
    //     return true;
    //
    // }
}