using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MonkeySpecies
    {
        Capuchin,
        Mandrill,
        Howler,
        Spider,
        Squirrel,
        Tamarin,
        Macaque,
        Marmoset,
        Colobus,
        Proboscis,
        Bonobo,
        Baboon,
        Vervet,
        Gibbon,
        Langur
    }

}