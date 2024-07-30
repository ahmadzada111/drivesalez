using PayPal.Api;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using DriveSalez.Application.ServiceContracts;
using System.Text;
using System.Text.Json.Serialization;
using DriveSalez.SharedKernel.Settings;

namespace DriveSalez.Application.Services;

public class PayPalService : IPayPalService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly PayPalSettings _payPalSettings;

    public PayPalService(IHttpClientFactory httpClientFactory, IOptions<PayPalSettings> payPalSettings)
    {
        _httpClientFactory = httpClientFactory;
        _payPalSettings = payPalSettings.Value;
    }

    private async Task<string> GetAccessTokenAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var byteArray = Encoding.ASCII.GetBytes($"{_payPalSettings.ClientId}:{_payPalSettings.Secret}");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

        var response = await client.PostAsync("https://api-m.sandbox.paypal.com/v1/oauth2/token", formContent);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var accessTokenResponse = JsonSerializer.Deserialize<PayPalAccessTokenResponse>(jsonResponse);

        return accessTokenResponse.AccessToken;
    }

    public async Task<Order> CreateOrderAsync(string currency, decimal value, string returnUrl, string cancelUrl)
    {
        var client = _httpClientFactory.CreateClient();
        var accessToken = await GetAccessTokenAsync();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var orderRequest = new
        {
            intent = "CAPTURE",
            purchase_units = new[]
            {
                new
                {
                    amount = new
                    {
                        currency_code = currency,
                        value = value.ToString("F2")
                    }
                }
            },
            application_context = new
            {
                return_url = returnUrl,
                cancel_url = cancelUrl
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(orderRequest), System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://api-m.sandbox.paypal.com/v2/checkout/orders", content);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Order>(responseContent, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}

public class PayPalAccessTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
}
