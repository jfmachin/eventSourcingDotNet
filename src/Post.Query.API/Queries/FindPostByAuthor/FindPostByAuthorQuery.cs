using MediatR;
using Post.Query.Domain.Entities;

namespace Post.Query.API.Queries.FindAllPosts;
public class FindPostByAuthorQuery : IRequest<List<PostEntity>> {
    public string Author { get; set; }
}