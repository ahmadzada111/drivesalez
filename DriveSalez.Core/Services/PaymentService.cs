using System.Globalization;
using DriveSalez.Core.DTO;
using DriveSalez.Core.ServiceContracts;
using Microsoft.Extensions.Configuration;
using PayPal.Api;

namespace DriveSalez.Core.Services;

public class PaymentService : IPaymentService
{
    private readonly IConfiguration _configuration;
    
    public PaymentService(IConfiguration configuration)
    {
        _configuration = configuration;
       
    }

    public bool ProcessPayment(PaymentRequestDto request)
    {
        // try
        // {
        //     var config = new Dictionary<string, string>
        //     {
        //         { "mode", "sandbox" },
        //         { "clientId", _configuration["PayPal:ClientId"] },
        //         { "clientSecret", _configuration["PayPal:Secret"] }
        //     };
        //
        //     var accessToken = new OAuthTokenCredential(config).GetAccessToken();
        //
        //     var apiContext = new APIContext(accessToken);
        //
        //     var payment = new Payment
        //     {
        //         intent = "sale",
        //         payer = new Payer { payment_method = "credit_card", funding_instruments = new List<FundingInstrument>() },
        //         transactions = new List<Transaction>
        //         {
        //             new Transaction
        //             {
        //                 amount = new Amount { currency = "USD", total = (request.AmountInCents / 100.0).ToString("F2", CultureInfo.InvariantCulture) },
        //                 payee = new Payee {email = _configuration["PayPal:Email"]},
        //             }
        //         }
        //     };
        //
        //     var card = new CreditCard
        //     {
        //         number = request.CardNumber,
        //         expire_month = request.ExpireMonth,
        //         expire_year = request.ExpireYear,
        //         cvv2 = request.Cvv,
        //         first_name = request.FirstName,
        //         last_name = request.LastName,
        //         type = "visa"
        //     };
        //
        //     payment.payer.funding_instruments.Add(new FundingInstrument { credit_card = card });
        //
        //     var createdPayment = payment.Create(apiContext);
        //     
        //     return true;
        // }
        // catch (Exception ex)
        // {
        //     return false;
        // }

        return true;
    }
}