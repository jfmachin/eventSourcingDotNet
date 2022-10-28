using MediatR;
using Post.Query.Domain.Entities;

namespace Post.Query.API.Queries.FindAllPosts;
public class FindAllPostsQuery : IRequest<List<PostEntity>> {

}