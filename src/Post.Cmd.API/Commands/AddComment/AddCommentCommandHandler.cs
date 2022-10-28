using CQRS.Core.Handlers;
using MediatR;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.API.Commands.AddComment;
public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand> {
    private readonly IEventSourcingHandler<PostAggregate> handler;

    public AddCommentCommandHandler(IEventSourcingHandler<PostAggregate> handler) {
        this.handler = handler;
    }

    public async Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken) {
        var aggregate = await handler.GetByIdAsync(request.Id);
        aggregate.AddComment(request.Comment, request.Username);
        await handler.SaveAsync(aggregate);
        return Unit.Value;
    }
}
