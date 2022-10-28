using MediatR;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;

namespace Post.Query.API.Queries.FindAllPosts;
public class FindAllPostsQueryHandler : IRequestHandler<FindAllPostsQuery, List<PostEntity>> {
    private readonly IPostRepository postRepository;

    public FindAllPostsQueryHandler(IPostRepository postRepository) {
        this.postRepository = postRepository;
    }

    public async Task<List<PostEntity>> Handle(FindAllPostsQuery request, CancellationToken cancellationToken) {
        return await postRepository.ListAllAsync();
    }
}