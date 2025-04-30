namespace Domain.DatabaseModels
{
public class ShelterManagerDbModel
{
    public ShelterManagerDbModel(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public ShelterDbModel Shelter { get; set; }
    public int ShelterId { get; set; }
}

}