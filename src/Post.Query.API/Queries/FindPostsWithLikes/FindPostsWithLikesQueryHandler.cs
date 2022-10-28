using MediatR;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;

namespace Post.Query.API.Queries.FindAllPosts;
public class FindPostsWithLikesQueryHandler : IRequestHandler<FindPostsWithLikesQuery, List<PostEntity>> {
    private readonly IPostRepository postRepository;

    public FindPostsWithLikesQueryHandler(IPostRepository postRepository) {
        this.postRepository = postRepository;
    }
    public async Task<List<PostEntity>> Handle(FindPostsWithLikesQuery request, CancellationToken cancellationToken) {
        return await postRepository.ListWithLikesAsync(request.NumberOfLikes);
    }
}