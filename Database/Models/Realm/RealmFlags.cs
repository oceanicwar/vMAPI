namespace vMAPI.Database.Models.Realm;

public enum RealmFlags : int
{
    Invalid = 0x1,
    Offline = 0x2,
    ShowVersionAndBuild = 0x4,
    NewPlayers = 0x20,
    Recommended = 0x40
}
