using Confluent.Kafka;
using CQRS.Core.Consumers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Repositories;
using Post.Query.Infrastructure.Consumers;
using Post.Query.Infrastructure.Data;
using Post.Query.Infrastructure.Handlers;
using Post.Query.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("sqlserver"),
    sqlServerOptionsAction: opt => {
        opt.EnableRetryOnFailure();
    }));

builder.Services.AddScoped<IPostRepository, PostRespository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IEventHandler, Post.Query.Infrastructure.Handlers.EventHandler>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));
builder.Services.AddSingleton<IEventConsumer, EventConsumer>();

builder.Services.AddHostedService<ConsumerBackgroundService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();