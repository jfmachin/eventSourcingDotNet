using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;
using Post.Query.Infrastructure.Data;

namespace Post.Query.Infrastructure.Repositories;
public class PostRespository : IPostRepository {
    private readonly AppDbContext context;

    public PostRespository(AppDbContext context) {
        this.context = context;
    }
    public async Task CreateAsync(PostEntity post) {
        context.Posts.Add(post);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid postId) {
        var post = await GetByIdAsync(postId);
        if (post == null) return;

        context.Posts.Remove(post);
        await context.SaveChangesAsync();
    }

    public async Task<PostEntity> GetByIdAsync(Guid postId) {
        return await context.Posts.Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.PostId == postId);
    }

    public async Task<List<PostEntity>> ListAllAsync() {
        return await context.Posts.AsNoTracking()
            .Include(x => x.Comments)
            .ToListAsync();
    }

    public async Task<List<PostEntity>> ListByAuthorAsync(string author) {
        return await context.Posts.AsNoTracking()
            .Include(x => x.Comments)
            .Where(x => x.Author.Contains(author))
            .ToListAsync();
    }

    public async Task<List<PostEntity>> ListWithCommentAsync() {
        return await context.Posts.AsNoTracking()
            .Include(x => x.Comments)
            .Where(x => x.Comments != null && x.Comments.Any())
            .ToListAsync();
    }

    public async Task<List<PostEntity>> ListWithLikesAsync(int numberOfLikes) {
        return await context.Posts.AsNoTracking()
            .Include(x => x.Comments)
            .Where(x => x.Likes >= numberOfLikes)
            .ToListAsync();
    }

    public async Task UpdateAsync(PostEntity post) {
        context.Posts.Update(post);
        await context.SaveChangesAsync();
    }
}
