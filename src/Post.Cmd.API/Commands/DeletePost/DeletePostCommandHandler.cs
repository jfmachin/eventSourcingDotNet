using CQRS.Core.Handlers;
using MediatR;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.API.Commands.DeletePost;
public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand> {
    private readonly IEventSourcingHandler<PostAggregate> handler;

    public DeletePostCommandHandler(IEventSourcingHandler<PostAggregate> handler) {
        this.handler = handler;
    }

    public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken) {
        var aggregate = await handler.GetByIdAsync(request.Id);
        aggregate.DeletePost(request.Username);
        await handler.SaveAsync(aggregate);
        return Unit.Value;
    }
}
