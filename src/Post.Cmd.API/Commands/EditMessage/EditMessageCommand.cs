using CQRS.Core.Commands;
using MediatR;

namespace Post.Cmd.API.Commands.EditMessage;
public class EditMessageCommand : BaseCommand, IRequest {
    public string Message { get; set; }
}
