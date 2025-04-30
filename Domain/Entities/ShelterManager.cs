using CSharpFunctionalExtensions;

namespace Domain.Entities
{
    public class ShelterManager
    {
    public string UserName { get; }

    private ShelterManager(string username)
    {
        UserName = username;
    }

    public static Result<ShelterManager> CreateShelterManager(Maybe<string> username)
    {
        var result = username.ToResult("Username must not be null or empty");
        if (result.IsFailure)
            return Result.Failure<ShelterManager>(result.Error);

        return Result.Success(new ShelterManager(result.Value));
    }
}

}
