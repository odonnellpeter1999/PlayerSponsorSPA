using Microsoft.IdentityModel.Tokens;
using PlayerSponsor.Server.Common;
using PlayerSponsor.Server.Services.ClubService;
using PlayerSponsor.Server.Services.Commands;
using PlayerSponsor.Server.Services.SessionService;
using Stripe;
using Stripe.Checkout;

namespace PlayerSponsor.Server.Services.CheckoutService;

public class CheckoutService : ICheckoutService
{
    private readonly IConfiguration _configuration;
    private IClubService _clubService;
    private ISessionService _sessionService;

    public CheckoutService(IConfiguration configuration, IClubService clubService, ISessionService sessionService)
    {
        _configuration = configuration;
        _clubService = clubService;
        _sessionService = sessionService;

        StripeConfiguration.ApiKey = _configuration["Stripe:ApiKey"];
    }

    public async Task<ResultT<string>> CreateCheckoutSessionAsync(CreateCheckoutSessionCommand command)
    {
        var result = await _clubService.GetClubBySlugAsync(command.ClubSlug);

        if (!result.IsSuccess)
        {
            return result.Error;
        }

        var club = result.Value;

        var checkoutProducts = club.Products.Where(p => command.ProductIds.Contains(p.Id)).ToList();

        if (checkoutProducts.IsNullOrEmpty() || checkoutProducts.Count < command.ProductIds.Count())
        {
            return CheckoutServiceError.InvalidProducts();
        }

        var sessionUrl = CreateCheckoutSessionUrl(checkoutProducts, CreateSuccessUrl(club.Slug), CreateCancelUrl(club.Slug), club.Currency);
        return sessionUrl;
    }

    private string CreateCheckoutSessionUrl(List<Models.Product> checkoutProducts, string successUrl, string cancelUrl, string currency)
    {
        var sessionLineItems = checkoutProducts.Select(p => CreateSessionLineItem(p, currency)).ToList();

        var options = new SessionCreateOptions
        {
            LineItems = sessionLineItems,
            Mode = "payment",
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl
        };

        Session session = _sessionService.Create(options);
        return session.Url;
    }

    private SessionLineItemOptions CreateSessionLineItem(Models.Product product, string currency)
    {
        return new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmount = product.PriceUnit,
                Currency = currency,
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = product.Name,
                    Description = product.Description,
                },
            },
            Quantity = 1,
        };
    }

    private string CreateSuccessUrl(string slug)
    {
        var host = _configuration["Host"];

        return $"{host}/{slug}/checkout/success";
    }

    private string CreateCancelUrl(string slug)
    {
        var host = _configuration["Host"];

        return $"{host}/{slug}/checkout/cancel";
    }
}
