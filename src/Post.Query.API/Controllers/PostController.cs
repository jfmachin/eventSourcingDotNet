using MediatR;
using Microsoft.AspNetCore.Mvc;
using Post.Common.DTOs;
using Post.Query.API.DTOs;
using Post.Query.API.Queries.FindAllPosts;
using Post.Query.Domain.Entities;

namespace Post.Query.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PostController : ControllerBase {
    private readonly ILogger<PostController> logger;
    private readonly IMediator mediator;

    public PostController(ILogger<PostController> logger, IMediator mediator) {
        this.logger = logger;
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> getAllPost() {
        try {
            var posts = await mediator.Send(new FindAllPostsQuery());
            return NormalResponse(posts);
        }
        catch (Exception ex) {
            var errorMsg = "Error while processing request to retrieve all the posts";
            return ErrorResponse(errorMsg, ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> getById(Guid id) {
        try {
            var post = await mediator.Send(new FindPostByIdQuery { Id = id });
            return NormalResponse(new List<PostEntity> { post });
        }
        catch (Exception ex) {
            var errorMsg = "Error while processing request to retrieve the post";
            return ErrorResponse(errorMsg, ex);
        }
    }

    [HttpGet("byAuthor/{author}")]
    public async Task<ActionResult> getByAuthor(string author) {
        try {
            var posts = await mediator.Send(new FindPostByAuthorQuery { Author = author });
            return NormalResponse(posts);
        }
        catch (Exception ex) {
            var errorMsg = $"Error while processing request to retrieve the posts authored by {author}";
            return ErrorResponse(errorMsg, ex);
        }
    }

    

    [HttpGet("withcomments")]
    public async Task<ActionResult> getWithComments() {
        try {
            var posts = await mediator.Send(new FindPostsWithCommentsQuery());
            return NormalResponse(posts);
        }
        catch (Exception ex) {
            var errorMsg = $"Error while processing request to retrieve the posts with comments";
            return ErrorResponse(errorMsg, ex);
        }
    }

    [HttpGet("withlikes/{n}")]
    public async Task<ActionResult> getWithLikes(int n) {
        try {
            var posts = await mediator.Send(new FindPostsWithLikesQuery { NumberOfLikes = n});
            return NormalResponse(posts);
        }
        catch (Exception ex) {
            var errorMsg = $"Error while processing request to retrieve the posts with {n} likes";
            return ErrorResponse(errorMsg, ex);
        }
    }

    private ActionResult NormalResponse(List<PostEntity> posts) {
        if (posts == null || !posts.Any())
            return NoContent();

        var count = posts.Count;
        return Ok(new PostLookupResponse
        {
            Posts = posts,
            Message = $"Successfully returned {count} post{(count > 1 ? "s" : string.Empty)}!"
        });
    }

    private ActionResult ErrorResponse(string errorMsg, Exception ex) {
        logger.LogError(ex, errorMsg);
        return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse {
            Message = errorMsg
        });
    }
}