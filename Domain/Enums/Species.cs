using System.Text.Json.Serialization;

namespace Domain.Enums
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