using PlayerSponsor.Server.Models;

namespace PlayerSponsor.Server.Services;

public class RegisterUserResult;

public class RegisterUserSuccess: RegisterUserResult
{
    public ApplicationUser AplicationUser;

    public RegisterUserSuccess(ApplicationUser aplicationUser)
    {
        AplicationUser = aplicationUser;
    }
}

public class  RegisterUserFailure: RegisterUserResult
{
    public Dictionary<string, string> Errors;

    public RegisterUserFailure(Dictionary<string, string>? errors)
    {
        Errors = errors ?? [];
    }
}