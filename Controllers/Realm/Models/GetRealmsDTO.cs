namespace vMAPI.Controllers.Realm.Models;

public class GetRealmsDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Address { get; set; }
    public int Port { get; set; }
}
