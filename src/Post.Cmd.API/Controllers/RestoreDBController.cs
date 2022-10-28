using MediatR;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.API.Commands.RestoreDB;
using Post.Common.DTOs;

namespace Post.Cmd.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class RestoreDBController : ControllerBase {
    private readonly ILogger<RestoreDBController> logger;
    private readonly IMediator mediator;

    public RestoreDBController(ILogger<RestoreDBController> logger, IMediator mediator) {
        this.logger = logger;
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> restore() {
        try {
            await mediator.Send(new RestoreDBCommand());
            return StatusCode(StatusCodes.Status201Created, new BaseResponse {
                Message = "Read database restpre request completeted successfully!"
            });
        }
        catch (InvalidOperationException ex) {
            logger.LogWarning(ex, "Bad request!");
            return BadRequest(new BaseResponse {
                Message = ex.Message
            });
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error while processing request to restore read database!");
            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse {
                Message = ex.Message
            });
        }
    }
}