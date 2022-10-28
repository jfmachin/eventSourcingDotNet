using CQRS.Core.Commands;
using MediatR;

namespace Post.Cmd.API.Commands.RemoveComment;
public class RemoveCommentCommand : BaseCommand, IRequest {
    public Guid CommentId { get; set; }
    public string Username { get; set; }
}