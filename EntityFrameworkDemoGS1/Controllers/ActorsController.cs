using AutoMapper;
using EntityFrameworkDemoGS1.DTOs;
using EntityFrameworkDemoGS1.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDemoGS1.Controllers;

[Route("api/actors")]
[ApiController]
public class ActorsController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    public ActorsController(ApplicationDbContext _context, IMapper _mapper)
    {
        context = _context;
        mapper = _mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Actor>>> Get()
    {
        return await context.Actors.ToListAsync();
    }

    [HttpGet("name")]
    public async Task<ActionResult<IEnumerable<Actor>>> Get(string name)
    {
        return await context.Actors.Where(n => n.Name == name).ToListAsync();
    }

    [HttpGet("name/any")]
    public async Task<ActionResult<IEnumerable<Actor>>> GetName(string name)
    {
        return await context
            .Actors
            .Where(n => n.Name.Contains(name))
            .OrderBy(x => x.Name)
                .ThenByDescending(y => y.DateOfBirth)
            .ToListAsync();
    }

    [HttpGet("idandname")]
    public async Task<ActionResult> GetIdAndName()
    {
        var actors = await context.Actors.Select(i => new { i.Id, i.Name }).ToListAsync();

        return Ok(actors);
    }

    [HttpGet("idandnamewithdto")]
    public async Task<ActionResult> GetIdAndNameWithDTO()
    {
        var actors = await context.Actors.Select(i => new ActorsGetDTO { Id = i.Id, Name = i.Name }).ToListAsync();

        return Ok(actors);
    }

    [HttpGet("{actorId:int}")]
    public async Task<ActionResult<Actor>> Get(int actorId)
    {
        var actor = await context.Actors.FirstOrDefaultAsync(a => a.Id == actorId);

        if (actor is null)
        {
            return NotFound();
        }
        return actor;
    }

    [HttpGet("dateofbirth/range")]
    public async Task<ActionResult<IEnumerable<Actor>>> GetByDOBRange(DateTime start, DateTime end)
    {
        return await context.Actors.Where(a => a.DateOfBirth >= start && a.DateOfBirth <= end).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult> Post(ActorCreationDTO actorDto)
    {
        var actor = mapper.Map<Actor>(actorDto);
        context.Add(actor);
        await context.SaveChangesAsync();
        return Ok();
    }
}
