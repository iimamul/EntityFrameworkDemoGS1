using AutoMapper;
using EntityFrameworkDemoGS1.DTOs;
using EntityFrameworkDemoGS1.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkDemoGS1.Controllers;

[ApiController]
[Route("api/movies/{movieId:int}/comments")]
public class CommentsController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    public CommentsController(ApplicationDbContext _context, IMapper _mapper)
    {
        context = _context;
        mapper = _mapper;
    }

    [HttpPost]
    public async Task<ActionResult> Post(int movieId, CommentCreationDTO commentCreationDTO)
    {
        var comment = mapper.Map<Comment>(commentCreationDTO);
        comment.MovieId = movieId;
        context.Add(comment);
        await context.SaveChangesAsync();
        return Ok();
    }



}
