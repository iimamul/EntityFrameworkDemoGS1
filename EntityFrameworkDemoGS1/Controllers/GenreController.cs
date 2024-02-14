using EntityFrameworkDemoGS1.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkDemoGS1.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public GenreController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Genre genre)
        {
            context.Add(genre);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
