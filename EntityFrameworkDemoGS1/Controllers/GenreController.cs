using AutoMapper;
using EntityFrameworkDemoGS1.DTOs;
using EntityFrameworkDemoGS1.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDemoGS1.Controllers;

[ApiController]
[Route("api/genres")]
public class GenreController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    public GenreController(ApplicationDbContext _context, IMapper _mapper)
    {
        context = _context;
        mapper = _mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> Get()
    {
        return await context.Genres.OrderByDescending(g => g.Name).ToListAsync();
    }


    [HttpPost]
    public async Task<ActionResult> Post(GenreCreationDTO genreDto)
    {
        var genre = mapper.Map<Genre>(genreDto);
        context.Add(genre);
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("multiple")]
    public async Task<ActionResult> Post(GenreCreationDTO[] genreDto)
    {
        var genres = mapper.Map<Genre[]>(genreDto);
        context.AddRange(genres);
        await context.SaveChangesAsync();
        return Ok();
    }

    //connected model because genre first loaded by context and same model will be updated
    [HttpPut("{id:int}/update")]
    public async Task<ActionResult<Actor>> Put(int id)
    {
        var genre = await context.Genres.FirstOrDefaultAsync(a => a.Id == id);

        if (genre is null)
        {
            return NotFound();
        }
        genre.Name += "2";
        await context.SaveChangesAsync();
        return Ok(genre);
    }

    //disconnected model because genre not loaded from context model
    [HttpPut("{id:int}/update2")]
    public async Task<ActionResult<Actor>> Put(int id, GenreCreationDTO genreDto)
    {
        var genre = mapper.Map<Genre>(genreDto);

        genre.Id = id;
        context.Update(genre);
        await context.SaveChangesAsync();
        return Ok(genre);
    }


    [HttpDelete("{id:int}/newway")]
    public async Task<ActionResult> Delete(int id)
    {
        var deletedRows = await context.Genres.Where(g => g.Id == id).ExecuteDeleteAsync();
        if (deletedRows == 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}/oldway")]
    public async Task<ActionResult> DeleteOldWay(int id)
    {
        var genre = await context.Genres.FirstOrDefaultAsync(g => g.Id == id);
        if (genre is null)
        {
            return NotFound();
        }

        context.Remove(genre);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
