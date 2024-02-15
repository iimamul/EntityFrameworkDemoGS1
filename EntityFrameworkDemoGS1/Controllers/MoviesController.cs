using AutoMapper;
using EntityFrameworkDemoGS1.DTOs;
using EntityFrameworkDemoGS1.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDemoGS1.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    public MoviesController(ApplicationDbContext _context, IMapper _mapper)
    {
        context = _context;
        mapper = _mapper;
    }

    [HttpPost]
    public async Task<ActionResult> Post(MovieCreationDTO movieCreationDto)
    {
        var movie = mapper.Map<Movie>(movieCreationDto);

        if (movie.Genres is not null)
        {
            foreach (var genre in movie.Genres)
            {
                //genres are already present in the db so we are telling ef core that their state will remain unchanged
                context.Entry(genre).State = EntityState.Unchanged;
            }
        }
        if (movie.MovieActors is not null)
        {
            for (int i = 0; i < movie.MovieActors.Count; i++)
            {
                movie.MovieActors[i].Order = i + 1;
            }
        }
        context.Add(movie);
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Movie>> Get(int id)
    {
        var movie = await context
            .Movies
            .Include(m => m.Comments)
            .Include(n => n.Genres.OrderByDescending(g => g.Id))
            .Include(p => p.MovieActors)
                .ThenInclude(pp => pp.Actor)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (movie is null)
        {
            return NotFound();
        }
        return movie;
    }

    [HttpGet("selectloading/{id:int}")]
    public async Task<ActionResult> GetSelect(int id)
    {
        //we can replace this belows anonymous type objects with a DTO
        var movie = await context
            .Movies
            .Select(mov => new
            {
                Id = mov.Id,
                Title = mov.Title,
                Genre = mov.Genres.Select(g => g.Name).ToList(),
                Actors = mov.MovieActors.OrderByDescending(ma => ma.ActorId).Select(ma => new
                {
                    Name = ma.Actor.Name,
                    Id = ma.ActorId,
                    Character = ma.Character
                }),
                CommentsQuantity = mov.Comments.Count()
            })
            .FirstOrDefaultAsync(a => a.Id == id);

        if (movie is null)
        {
            return NotFound();
        }
        return Ok(movie);
    }

    [HttpDelete("{id:int}/newway")]
    public async Task<ActionResult> Delete(int id)
    {
        var deletedRows = await context.Movies.Where(g => g.Id == id).ExecuteDeleteAsync();
        if (deletedRows == 0)
        {
            return NotFound();
        }

        return NoContent();
    }

}
