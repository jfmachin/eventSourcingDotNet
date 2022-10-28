using CQRS.Core.Events;
using MongoDB.Driver;

namespace Post.Cmd.Infrastructure.Data;

public interface IEventDbContext {
    public IMongoCollection<EventModel> Events { get; }
}