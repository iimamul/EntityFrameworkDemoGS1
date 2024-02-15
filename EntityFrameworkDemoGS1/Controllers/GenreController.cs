using AutoMapper;
using EntityFrameworkDemoGS1.DTOs;
using EntityFrameworkDemoGS1.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkDemoGS1.Controllers
{
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
    }
}
