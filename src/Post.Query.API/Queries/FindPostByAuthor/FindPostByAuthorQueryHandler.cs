using MediatR;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;

namespace Post.Query.API.Queries.FindAllPosts;
public class FindPostByAuthorQueryHandler : IRequestHandler<FindPostByAuthorQuery, List<PostEntity>> {
    private readonly IPostRepository postRepository;

    public FindPostByAuthorQueryHandler(IPostRepository postRepository) {
        this.postRepository = postRepository;
    }
    public async Task<List<PostEntity>> Handle(FindPostByAuthorQuery request, CancellationToken cancellationToken) {
        return await postRepository.ListByAuthorAsync(request.Author);
    }
}