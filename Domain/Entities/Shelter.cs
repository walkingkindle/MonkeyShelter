using CSharpFunctionalExtensions;

namespace Domain.Entities
{
public class Shelter
{
    public int ShelterManagerId { get; }

    private Shelter(int shelterManagerId)
    {
        ShelterManagerId = shelterManagerId;
    }

    public static Result<Shelter> CreateShelter(int shelterManagerId)
    {
        if (shelterManagerId <= 0)
        {
            return Result.Failure<Shelter>("Shelter manager ID must be a positive number.");
        }

        return Result.Success(new Shelter(shelterManagerId));
    }
}


}
