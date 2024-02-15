using AutoMapper;
using EntityFrameworkDemoGS1.DTOs;
using EntityFrameworkDemoGS1.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDemoGS1.Controllers
{
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

    }
}
