using MediatR;
using Post.Query.Domain.Entities;

namespace Post.Query.API.Queries.FindAllPosts;
public class FindPostByIdQuery : IRequest<PostEntity> {
    public Guid Id { get; set; }
}