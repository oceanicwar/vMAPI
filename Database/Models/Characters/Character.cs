using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace vMAPI.Database.Models.Characters;

[PrimaryKey(nameof(Id))]
[Table("characters")]
public class Character
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("online")]
    public bool Online { get; set; }
}
