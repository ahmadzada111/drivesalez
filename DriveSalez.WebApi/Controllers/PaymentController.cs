using DriveSalez.Core.DTO;
using DriveSalez.Core.Exceptions;
using DriveSalez.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.WebApi.Controllers;

public class PaymentController : Controller
{
    private readonly IPaymentService _paymentService;
 
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    [HttpPost("top-up-balance")]
    public async Task<ActionResult> TopUpBalance([FromBody] PaymentRequestDto request)
    {
        try
        {
            var result = await _paymentService.TopUpBalance(request);
            return result ? Ok() : BadRequest();
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return Problem();
        }
    }

    [HttpPost("add-premium-announcement-limit")]
    public async Task<ActionResult> AddPremiumAnnouncementLimit([FromBody] int announcementQuantity, int subscriptionId)
    {
        try
        {
            var result = await _paymentService.AddPremiumAnnouncementLimit(announcementQuantity, subscriptionId);
            return result ? Ok() : BadRequest();
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return Problem();
        }
    }

    [HttpPost("buy-premium-account")]
    public async Task<ActionResult> BuyPremiumAccount(int subscriptionId)
    {
        try
        {
            var result = await _paymentService.BuyPremiumAccount(subscriptionId);
            return result ? Ok() : BadRequest();
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return Problem();
        }
    }
    
    [HttpPost("buy-business-account")]
    public async Task<ActionResult> BuyBusinessAccount(int subscriptionId)
    {
        try
        {
            var result = await _paymentService.BuyBusinessAccount(subscriptionId);
            return result ? Ok() : BadRequest();
        }
        catch (UserNotAuthorizedException e)
        {
            return Unauthorized(e.Message);
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return Problem();
        }
    }
}