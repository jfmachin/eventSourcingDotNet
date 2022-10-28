using CQRS.Core.Handlers;
using MediatR;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.API.Commands.NewPost;
public class NewPostCommandHandler : IRequestHandler<NewPostCommand> {
    private readonly IEventSourcingHandler<PostAggregate> handler;

    public NewPostCommandHandler(IEventSourcingHandler<PostAggregate> handler) {
        this.handler = handler;
    }

    public async Task<Unit> Handle(NewPostCommand request, CancellationToken cancellationToken) {
        var aggregate = new PostAggregate(request.Id, request.Author, request.Message);
        await handler.SaveAsync(aggregate);
        return Unit.Value;
    }
}
