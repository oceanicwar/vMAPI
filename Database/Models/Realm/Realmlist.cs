using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace vMAPI.Database.Models.Realm;

[PrimaryKey(nameof(Id))]
[Table("realmlist")]
public class Realmlist
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("localAddress")]
    public string? LocalAddress { get; set; }

    [Column("localSubnetMask")]
    public string? LocalSubnetMask { get; set; }

    [Column("port")]
    public int Port { get; set; }

    [Column("icon")]
    public int Icon { get; set; }

    [Column("realmflags")]
    public int RealmFlags { get; set; }

    [Column("timezone")]
    public int TimeZone { get; set; }

    [Column("allowedSecurityLevel")]
    public int AllowedSecurityLevel { get; set; }

    [Column("population")]
    public float Population { get; set; }

    [Column("gamebuild_min")]
    public int GameBuildMin { get; set; }

    [Column("gamebuild_max")]
    public int GameBuildMax { get; set; }

    [Column("flag")]
    public int Flag { get; set; }

    [Column("realmbuilds")]
    public string? RealmBuilds { get; set; }
}
