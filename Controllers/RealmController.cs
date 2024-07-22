using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using vMAPI.Controllers.Models;
using vMAPI.Controllers.Models.Characters;
using vMAPI.Controllers.Models.Realm;
using vMAPI.Database;

namespace vMAPI.Controllers.Realm;

[ApiController]
[Route("[controller]")]
public class RealmController : Controller
{
    private readonly MangosRealmDbContext realmDbContext;
    private readonly MangosDbFactory dbFactory;

    public RealmController(MangosRealmDbContext realmDbContext, MangosDbFactory dbFactory)
    {
        this.realmDbContext = realmDbContext;
        this.dbFactory = dbFactory;
    }

    [AllowAnonymous]
    [HttpGet("list")]
    public async Task<IActionResult> ListRealmsAsync()
    {
        var realms = await realmDbContext.Realms.ToListAsync();

        return Json(realms.Select(r => new RealmlistDTO()
        {
            Id = r.Id,
            Name = r.Name,
            Address = r.Address,
            Port = r.Port
        }));
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> ListOnlinePlayersAsync(int? id)
    {
        if(id is null)
        {
            return BadRequest(new BasicApiResult(false, "You must supply a realm id."));
        }

        var realm = await realmDbContext.Realms.FirstOrDefaultAsync(r => r.Id == id);
        if(realm is null)
        {
            return BadRequest(new BasicApiResult(false, "No realm found with that id."));
        }

        var charDbContext = dbFactory.CreateCharactersDbContext(id.Value);
        if(charDbContext is null)
        {
            return StatusCode(500, new BasicApiResult(false, "An internal error occured."));
        }

        var characters = await charDbContext.Characters.Where(c => c.Online).ToListAsync();

        return Json(characters.Select(c => new CharacterDTO()
        {
            Name = c.Name,
            Level = c.Level
        }));
    }
}
