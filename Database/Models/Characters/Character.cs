using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;

namespace vMAPI.Database.Models.Characters;

[PrimaryKey(nameof(Id))]
[Table("characters")]
public class Character
{
    [Column("guid")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("online")]
    public bool Online { get; set; }

    [Column("level")]
    public int Level { get; set; }

    [Column("race")]
    public PlayerRace Race { get; set; }

    [Column("class")]
    public PlayerClass Class { get; set; }

    [Column("gender")]
    public PlayerGender Gender { get; set; }
}
