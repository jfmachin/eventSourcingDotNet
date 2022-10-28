using CQRS.Core.Events;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Post.Cmd.Infrastructure.Data;
public class EventDbContext : IEventDbContext {
    public EventDbContext(IConfiguration configuration) {
        var client = new MongoClient(configuration["MongoDbConfig:ConnectionString"]);
        var database = client.GetDatabase(configuration["MongoDbConfig:DatabaseName"]);
        Events = database.GetCollection<EventModel>(configuration["MongoDbConfig:CollectionName"]);
    }
    public IMongoCollection<EventModel> Events { get; }
}