using Microsoft.AspNetCore.Mvc;
using PlayerSponsor.Server.Common;
using PlayerSponsor.Server.Controllers.Requests;
using PlayerSponsor.Server.Services.CheckoutService;
using PlayerSponsor.Server.Services.Commands;

namespace PlayerSponsor.Server.Controllers;

[ApiController]
public class CheckoutController : BaseController
{
    private readonly ICheckoutService _checkoutService;

    public CheckoutController(ICheckoutService checkoutService)
    {
        _checkoutService = checkoutService;
    }

    [HttpPost]
    [Route("create-checkout-session")]
    public async Task<IActionResult> Create(CreateCheckoutSessionRequest request)
    {
        var command = new CreateCheckoutSessionCommand
        {
            ClubSlug = request.ClubSlug,
            ProductIds = request.ProductIds
        };

        var result = await _checkoutService.CreateCheckoutSessionAsync(command);

        if (!result.IsSuccess)
        {
            return Problem(result.Error);
        }

        return Ok(result);
    }
}