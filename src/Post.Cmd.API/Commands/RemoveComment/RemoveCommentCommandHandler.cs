using CQRS.Core.Handlers;
using MediatR;
using Post.Cmd.API.Commands.RemoveComment;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.API.Commands.DeletePost;
public class RemoveCommentCommandHandler : IRequestHandler<RemoveCommentCommand> {
    private readonly IEventSourcingHandler<PostAggregate> handler;

    public RemoveCommentCommandHandler(IEventSourcingHandler<PostAggregate> handler) {
        this.handler = handler;
    }

    public async Task<Unit> Handle(RemoveCommentCommand request, CancellationToken cancellationToken) {
        var aggregate = await handler.GetByIdAsync(request.Id);
        aggregate.RemoveComment(request.CommentId, request.Username);
        await handler.SaveAsync(aggregate);
        return Unit.Value;
    }
}
