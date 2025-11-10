using PlayerSponsor.Server.Common;

namespace PlayerSponsor.Server.Services.ClubService;

public static class ClubServiceError
{
    public static Error NotFound(string id) =>
        Error.NotFound("Club.NotFound", $"Club with Id: {id} not found");

    public static Error NotFound() =>
        Error.NotFound("Club.NotFound", $"Clubs not found");

    public static Error Conflict(string name) =>
        Error.Conflict("Club.Conflict", $"Club with Name: {name} already exists");

    public static Error CreateFailure =>
        Error.Failure("Club.CreateFailure", $"Something went wrong in creating the club");

    public static Error UpdateFailure =>
        Error.Failure("Club.UpdateFailure", $"Something went wrong in updating the club");

    public static Error DeleteFailure =>
        Error.Failure("Club.DeleteFailure", $"Something went wrong in deleting the club");
}
