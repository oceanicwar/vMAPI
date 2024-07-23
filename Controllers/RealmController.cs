﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using vMAPI.Controllers.Models;
using vMAPI.Controllers.Models.Characters;
using vMAPI.Controllers.Models.Realm;
using vMAPI.Database;
using vMAPI.Database.Models.Realm;
using vMAPI.Extensions;

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
        var filteredRealms = realms.Where(r => !(BitExtensions.HasFlag(r.RealmFlags, (int)RealmFlags.Invalid))).Select(r => new RealmlistDTO()
        {
            Id = r.Id,
            Name = r.Name,
            Address = r.Address,
            Port = r.Port
        }).ToList();

        return Ok(new BasicApiResult<List<RealmlistDTO>>(true, $"Found '{filteredRealms.Count}' realm(s).", filteredRealms));
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [ResponseCache(Duration = 120)]
    public async Task<IActionResult> ListOnlinePlayersAsync(int? id)
    {
        if(id is null)
        {
            return BadRequest(new BasicApiResult<object>(false, "You must supply a realm id."));
        }

        var realm = await realmDbContext.Realms.FirstOrDefaultAsync(r => r.Id == id);
        if(realm is null)
        {
            return BadRequest(new BasicApiResult<object>(false, "No realm found with that id."));
        }

        var isOnline = await realm.TestConnectionAsync();
        if(!isOnline)
        {
            return Ok(new BasicApiResult<object>(false, "Realm is offline."));
        }

        var charDbContext = dbFactory.CreateCharactersDbContext(id.Value);
        if(charDbContext is null)
        {
            return StatusCode(500, new BasicApiResult<object>(false, "An internal error occured."));
        }

        var characters = await charDbContext.Characters.Where(c => c.Online).Select(c => new CharacterDTO()
        {
            Name = c.Name,
            Level = c.Level,
            Class = c.Class,
            Gender = c.Gender,
            Race = c.Race
            
        }).ToListAsync();

        return Ok(new BasicApiResult<List<CharacterDTO>>(true, $"Found '{characters.Count}' online characters.", characters));
    }
}
