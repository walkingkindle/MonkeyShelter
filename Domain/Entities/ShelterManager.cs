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

    public static Result<ShelterManager> Create(Maybe<string> username)
    {
        return username.ToResult("username cannot be null")
            .Map(username => new ShelterManager(username));
    }
}

}
