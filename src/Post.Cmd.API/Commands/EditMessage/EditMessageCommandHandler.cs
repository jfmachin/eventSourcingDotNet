using CQRS.Core.Handlers;
using MediatR;
using Post.Cmd.API.Commands.EditMessage;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.API.Commands.EditComment;
public class EditMesssageCommandHandler : IRequestHandler<EditMessageCommand> {
    private readonly IEventSourcingHandler<PostAggregate> handler;

    public EditMesssageCommandHandler(IEventSourcingHandler<PostAggregate> handler) {
        this.handler = handler;
    }

    public async Task<Unit> Handle(EditMessageCommand request, CancellationToken cancellationToken) {
        var aggregate = await handler.GetByIdAsync(request.Id);
        aggregate.EditMessage(request.Message);
        await handler.SaveAsync(aggregate);
        return Unit.Value;
    }
}
