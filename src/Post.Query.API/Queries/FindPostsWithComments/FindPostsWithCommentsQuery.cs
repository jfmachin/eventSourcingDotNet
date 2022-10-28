using MediatR;
using Post.Query.Domain.Entities;

namespace Post.Query.API.Queries.FindAllPosts;
public class FindPostsWithCommentsQuery : IRequest<List<PostEntity>> {

}