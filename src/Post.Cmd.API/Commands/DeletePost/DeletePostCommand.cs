using CQRS.Core.Commands;
using MediatR;

namespace Post.Cmd.API.Commands.DeletePost;
public class DeletePostCommand : BaseCommand, IRequest {
    public string Username { get; set; }
}
