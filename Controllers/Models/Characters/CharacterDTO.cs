using System.ComponentModel.DataAnnotations.Schema;

using vMAPI.Database.Models.Characters;

namespace vMAPI.Controllers.Models.Characters;

public class CharacterDTO
{
    public string? Name { get; set; }
    public int Level { get; set; }

    public PlayerRace Race { get; set; }
    public PlayerClass Class { get; set; }
    public PlayerGender Gender { get; set; }
}
