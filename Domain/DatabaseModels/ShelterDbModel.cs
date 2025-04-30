namespace Domain.DatabaseModels
{
    public class ShelterDbModel
{
    public int Id { get; set; }

    public List<MonkeyDbModel> Monkeys { get; set; } = new();

    public int ShelterManagerId { get; set; }  // FK

    public ShelterManagerDbModel ShelterManager { get; set; }

    public ShelterDbModel(int shelterManagerId)
    {
        ShelterManagerId = shelterManagerId;
    }
}

}