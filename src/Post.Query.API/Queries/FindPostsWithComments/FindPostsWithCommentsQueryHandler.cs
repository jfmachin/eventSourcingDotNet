using MediatR;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;

namespace Post.Query.API.Queries.FindAllPosts;
public class FindPostsWithCommentsQueryHandler : IRequestHandler<FindPostsWithCommentsQuery, List<PostEntity>> {
    private readonly IPostRepository postRepository;

    public FindPostsWithCommentsQueryHandler(IPostRepository postRepository) {
        this.postRepository = postRepository;
    }
    public async Task<List<PostEntity>> Handle(FindPostsWithCommentsQuery request, CancellationToken cancellationToken) {
        return await postRepository.ListWithCommentAsync();
    }
}