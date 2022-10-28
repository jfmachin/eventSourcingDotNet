using MediatR;
using Post.Query.Domain.Entities;

namespace Post.Query.API.Queries.FindAllPosts;
public class FindPostsWithLikesQuery : IRequest<List<PostEntity>> {
    public int NumberOfLikes { get; set; }
}