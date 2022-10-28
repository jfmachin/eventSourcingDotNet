using CQRS.Core.Handlers;
using MediatR;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.API.Commands.RestoreDB;
public class RestoreDBCommandHandler : IRequestHandler<RestoreDBCommand> {
    private readonly IEventSourcingHandler<PostAggregate> handler;

    public RestoreDBCommandHandler(IEventSourcingHandler<PostAggregate> handler) {
        this.handler = handler;
    }
    public async Task<Unit> Handle(RestoreDBCommand request, CancellationToken cancellationToken) {
        await handler.RepublishEventsAsync();
        return Unit.Value;
    }
}