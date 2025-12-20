using Stripe.Checkout;

namespace PlayerSponsor.Server.Services.SessionService;

public interface ISessionService
{
    Session Create(SessionCreateOptions options);
}