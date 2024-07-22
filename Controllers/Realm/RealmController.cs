using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using vMAPI.Controllers.Realm.Models;
using vMAPI.Database;

namespace vMAPI.Controllers.Realm;

[ApiController]
[Route("[controller]")]
public class RealmController : Controller
{
    private readonly MangosRealmDbContext dbContext;

    public RealmController(MangosRealmDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [AllowAnonymous]
    [HttpGet("list")]
    public async Task<IActionResult> ListRealmsAsync()
    {
        var realms = await dbContext.Realms.ToListAsync();

        return Json(realms.Select(r => new GetRealmsDTO()
        {
            Id = r.Id,
            Name = r.Name,
            Address = r.Address,
            Port = r.Port
        }));
    }
}
