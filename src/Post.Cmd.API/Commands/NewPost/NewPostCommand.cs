using CQRS.Core.Commands;
using MediatR;

namespace Post.Cmd.API.Commands.NewPost;
public class NewPostCommand : BaseCommand, IRequest {
    public string Author { get; set; }
    public string Message { get; set; }
}
