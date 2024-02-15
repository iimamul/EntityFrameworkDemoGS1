using AutoMapper;
using EntityFrameworkDemoGS1.DTOs;
using EntityFrameworkDemoGS1.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkDemoGS1.Controllers
{
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

        [HttpPost]
        public async Task<ActionResult> Post(ActorCreationDTO actorDto)
        {
            var actor = mapper.Map<Actor>(actorDto);
            context.Add(actor);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
