using CQRS.Core.Handlers;
using MediatR;
using Post.Cmd.API.Commands.LikePost;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.API.Commands.DeletePost;
public class LikePostCommandHandler : IRequestHandler<LikePostCommand> {
    private readonly IEventSourcingHandler<PostAggregate> handler;

    public LikePostCommandHandler(IEventSourcingHandler<PostAggregate> handler) {
        this.handler = handler;
    }

    public async Task<Unit> Handle(LikePostCommand request, CancellationToken cancellationToken) {
        var aggregate = await handler.GetByIdAsync(request.Id);
        aggregate.LikePost();
        await handler.SaveAsync(aggregate);
        return Unit.Value;
    }
}
