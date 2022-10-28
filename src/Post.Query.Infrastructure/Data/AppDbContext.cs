using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;

namespace Post.Query.Infrastructure.Data;
public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<PostEntity> Posts { get; set; }
}