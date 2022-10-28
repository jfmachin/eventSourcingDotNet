using Post.Common.Events;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;

namespace Post.Query.Infrastructure.Handlers;

public class EventHandler : IEventHandler {
    private readonly IPostRepository postRespository;
    private readonly ICommentRepository commentRepository;

    public EventHandler(IPostRepository postRespository, ICommentRepository commentRepository) {
        this.postRespository = postRespository;
        this.commentRepository = commentRepository;
    }
    public async Task On(PostCreatedEvent @event) {
        var post = new PostEntity { 
            PostId = @event.Id,
            Author = @event.Author,
            DatePosted = @event.DatePosted,
            Message = @event.Message
        };
        await postRespository.CreateAsync(post);
    }

    public async Task On(CommentAddedEvent @event) {
        var comment = new CommentEntity {
            Comment = @event.Comment,
            CommentId = @event.CommentId,
            CommentDate = @event.CommentDate,
            PostId = @event.Id,
            Username = @event.Username,
            Edited = false
        };
        await commentRepository.CreateAsync(comment);
    }

    public async Task On(CommentRemovedEvent @event) {
        await commentRepository.DeleteAsync(@event.CommentId);
    }

    public async Task On(CommentUpdatedEvent @event) {
        var comment = await commentRepository.GetByIdAsync(@event.CommentId);
        if (comment == null) return;

        comment.Edited = true;
        comment.Comment = @event.Comment;
        comment.CommentDate = @event.EditDate;

        await commentRepository.UpdateAsync(comment);
    }

    public async Task On(MessageUpdatedEvent @event) {
        var post = await postRespository.GetByIdAsync(@event.Id);
        if (post == null) return;

        post.Message = @event.Message;
        await postRespository.UpdateAsync(post);
    }

    public async Task On(PostLikedEvent @event) {
        var post = await postRespository.GetByIdAsync(@event.Id);
        if (post == null) return;

        post.Likes++;
        await postRespository.UpdateAsync(post);
    }

    public async Task On(PostRemovedEvent @event) {
        await postRespository.DeleteAsync(@event.Id);
    }
}