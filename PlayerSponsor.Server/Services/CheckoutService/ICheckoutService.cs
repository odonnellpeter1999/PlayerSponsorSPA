using PlayerSponsor.Server.Common;
using PlayerSponsor.Server.Services.Commands;

namespace PlayerSponsor.Server.Services.CheckoutService;

public interface ICheckoutService
{
    Task<ResultT<string>> CreateCheckoutSessionAsync(CreateCheckoutSessionCommand command);
}