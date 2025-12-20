using PlayerSponsor.Server.Common;

namespace PlayerSponsor.Server.Services.CheckoutService;

public static class CheckoutServiceError
{
    public static Error NotFound(string id) => Error.NotFound("Club.NotFound", $"Club with Id: {id} not found");

    public static Error InvalidProducts() => Error.Validation("Checkout.InvalidProducts", "Invalid productIds included in request");
}
