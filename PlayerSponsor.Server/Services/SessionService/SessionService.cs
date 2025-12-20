using Stripe.Checkout;

namespace PlayerSponsor.Server.Services.SessionService;

/// <summary>
/// This is just a wrapper to improve unit testing.
/// </summary>
public class SessionService : ISessionService
{
    private readonly Stripe.Checkout.SessionService _sessionService;

    public SessionService(Stripe.Checkout.SessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public Session Create(SessionCreateOptions options)
    {
        return _sessionService.Create(options);
    }
}
