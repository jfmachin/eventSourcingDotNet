using CQRS.Core.Commands;
using MediatR;

namespace Post.Cmd.API.Commands.EditComment;
public class EditCommentCommand : BaseCommand, IRequest {
    public Guid CommentId { get; set; }
    public string Comment { get; set; }
    public string Username { get; set; }
}
