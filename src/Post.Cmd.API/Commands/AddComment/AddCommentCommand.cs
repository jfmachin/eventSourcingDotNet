using CQRS.Core.Commands;
using MediatR;

namespace Post.Cmd.API.Commands.AddComment;
public class AddCommentCommand : BaseCommand, IRequest {
    public string Comment { get; set; }
    public string Username { get; set; }
}
