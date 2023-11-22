using Microsoft.AspNetCore.Mvc;

namespace DriveSalez.WebApi.Controllers;

public class PaymentController : Controller
{
    [HttpPost("pay-for-subscription")]
    public ActionResult PayForSubscription()
    {
        return Ok();
    }
}