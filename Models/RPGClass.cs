using System.Text.Json.Serialization;

namespace DotNet_rpg.Models
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
   public enum RPGClass
    {
        knight = 1,
        Mage = 2,
        Cleric = 3

    }
}