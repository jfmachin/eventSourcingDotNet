using MediatR;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;

namespace Post.Query.API.Queries.FindAllPosts;
public class FindPostByIdQueryHandler : IRequestHandler<FindPostByIdQuery, PostEntity> {
    private readonly IPostRepository postRepository;

    public FindPostByIdQueryHandler(IPostRepository postRepository) {
        this.postRepository = postRepository;
    }

    public async Task<PostEntity> Handle(FindPostByIdQuery request, CancellationToken cancellationToken) {
        var post = await postRepository.GetByIdAsync(request.Id);
        return post;
    }
}