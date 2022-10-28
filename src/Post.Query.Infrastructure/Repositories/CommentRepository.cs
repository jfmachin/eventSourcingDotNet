using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;
using Post.Query.Infrastructure.Data;

namespace Post.Query.Infrastructure.Repositories;
public class CommentRepository : ICommentRepository {
    private readonly AppDbContext context;

    public CommentRepository(AppDbContext context) {
        this.context = context;
    }
    public async Task CreateAsync(CommentEntity comment) {
        context.Comments.Add(comment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid commentId) {
        var comment = await GetByIdAsync(commentId);
        if (comment == null) return;

        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
    }

    public async Task<CommentEntity> GetByIdAsync(Guid commentId) {
        return await context.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId);
    }

    public async Task UpdateAsync(CommentEntity comment) {
        context.Comments.Update(comment);
        await context.SaveChangesAsync();
    }
}