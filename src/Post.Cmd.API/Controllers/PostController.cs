using CQRS.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.API.Commands.AddComment;
using Post.Cmd.API.Commands.DeletePost;
using Post.Cmd.API.Commands.EditComment;
using Post.Cmd.API.Commands.EditMessage;
using Post.Cmd.API.Commands.LikePost;
using Post.Cmd.API.Commands.NewPost;
using Post.Cmd.API.Commands.RemoveComment;
using Post.Common.DTOs;

namespace Post.Cmd.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PostController : ControllerBase {
    private readonly ILogger<PostController> logger;
    private readonly IMediator mediator;

    public PostController(ILogger<PostController> logger, IMediator mediator) {
        this.logger = logger;
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> New([FromBody] NewPostCommand postCommand) {
        var id = Guid.NewGuid();
        try {
            postCommand.Id = id;
            await mediator.Send(postCommand);
            return StatusCode(StatusCodes.Status201Created, new NewPostResponse
            {
                Id = id,
                Message = "New post successfully completed"
            });
        }
        catch (InvalidOperationException ex) {
            logger.LogWarning(ex, "Bad request!");
            return BadRequest(new BaseResponse { 
                Message = ex.Message
            });
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error while processing request to create a new post!");
            return StatusCode(StatusCodes.Status500InternalServerError, new NewPostResponse { 
                Id = id,
                Message = ex.Message
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, DeletePostCommand command) {
        try {
            command.Id = id;
            await mediator.Send(command);
            return Ok(new BaseResponse {
                Message = "Delete post request completed successfully!"
            });
        }
        catch (InvalidOperationException ex) {
            logger.LogWarning(ex, "Bad request!");
            return BadRequest(new BaseResponse { 
                Message = ex.Message
            });
        }
        catch (AggregateNotFoundException ex) {
            logger.LogWarning(ex, "Couldn't retreive aggregate!");
            return BadRequest(new BaseResponse { 
                Message = ex.Message
            });
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error while processing request to delete a post!");
            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { 
                Message = ex.Message
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> EditMessage(Guid id, EditMessageCommand editMessageCommand) {
        try {
            editMessageCommand.Id = id;
            await mediator.Send(editMessageCommand);
            return Ok(new BaseResponse {
                Message = "Edit message request completed successfully!"
            });
        }
        catch (InvalidOperationException ex) {
            logger.LogWarning(ex, "Bad request!");
            return BadRequest(new BaseResponse { 
                Message = ex.Message
            });
        }
        catch (AggregateNotFoundException ex) {
            logger.LogWarning(ex, "Couldn't retreive aggregate!");
            return BadRequest(new BaseResponse { 
                Message = ex.Message
            });
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error while processing request to edit a post!");
            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { 
                Message = ex.Message
            });
        }
    }

    [HttpPut("like/{id}")]
    public async Task<ActionResult> Like(Guid id) {
        try {
            await mediator.Send(new LikePostCommand { Id = id });
            return Ok(new BaseResponse {
                Message = "Like post request completed successfully!"
            });
        }
        catch (InvalidOperationException ex) {
            logger.LogWarning(ex, "Bad request!");
            return BadRequest(new BaseResponse { 
                Message = ex.Message
            });
        }
        catch (AggregateNotFoundException ex) {
            logger.LogWarning(ex, "Couldn't retreive aggregate!");
            return BadRequest(new BaseResponse { 
                Message = ex.Message
            });
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error while processing request to like a post!");
            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { 
                Message = ex.Message
            });
        }
    }

    [HttpPut("addcomment/{id}")]
    public async Task<ActionResult> AddComment(Guid id, AddCommentCommand command) {
        try {
            command.Id = id;
            await mediator.Send(command);
            return Ok(new BaseResponse {
                Message = "Add comment request completed successfully!"
            });
        }
        catch (InvalidOperationException ex) {
            logger.LogWarning(ex, "Bad request!");
            return BadRequest(new BaseResponse { 
                Message = ex.Message
            });
        }
        catch (AggregateNotFoundException ex) {
            logger.LogWarning(ex, "Couldn't retreive aggregate!");
            return BadRequest(new BaseResponse { 
                Message = ex.Message
            });
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error while processing request to add comment to a post!");
            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { 
                Message = ex.Message
            });
        }
    }

    [HttpPut("editcomment/{id}")]
    public async Task<ActionResult> EditComment(Guid id, EditCommentCommand command) {
        try {
            command.Id = id;
            await mediator.Send(command);
            return Ok(new BaseResponse {
                Message = "Edit comment request completed successfully!"
            });
        }
        catch (InvalidOperationException ex) {
            logger.LogWarning(ex, "Bad request!");
            return BadRequest(new BaseResponse { 
                Message = ex.Message
            });
        }
        catch (AggregateNotFoundException ex) {
            logger.LogWarning(ex, "Couldn't retreive aggregate!");
            return BadRequest(new BaseResponse { 
                Message = ex.Message
            });
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error while processing request to edit comment on a post!");
            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { 
                Message = ex.Message
            });
        }
    }

    [HttpDelete("deletecomment/{id}")]
    public async Task<ActionResult> DeleteComment(Guid id, RemoveCommentCommand command) {
        try {
            command.Id = id;
            await mediator.Send(command);
            return Ok(new BaseResponse {
                Message = "Delete comment request completed successfully!"
            });
        }
        catch (InvalidOperationException ex) {
            logger.LogWarning(ex, "Bad request!");
            return BadRequest(new BaseResponse { 
                Message = ex.Message
            });
        }
        catch (AggregateNotFoundException ex) {
            logger.LogWarning(ex, "Couldn't retreive aggregate!");
            return BadRequest(new BaseResponse { 
                Message = ex.Message
            });
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error while processing request to remove comment from a post!");
            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse { 
                Message = ex.Message
            });
        }
    }
}