using CQRS.Core.Handlers;
using MediatR;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.API.Commands.EditComment;
public class EditCommentCommandHandler : IRequestHandler<EditCommentCommand> {
    private readonly IEventSourcingHandler<PostAggregate> handler;

    public EditCommentCommandHandler(IEventSourcingHandler<PostAggregate> handler) {
        this.handler = handler;
    }

    public async Task<Unit> Handle(EditCommentCommand request, CancellationToken cancellationToken) {
        var aggregate = await handler.GetByIdAsync(request.Id);
        aggregate.EditComment(request.CommentId, request.Comment, request.Username);
        await handler.SaveAsync(aggregate);
        return Unit.Value;
    }
}
