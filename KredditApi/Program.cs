using KredditApi.Models;
using KredditApi.Services;
using KredditApi.Contexts;
using shared.Model;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PostContext>();
builder.Services.AddScoped<PostService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("http://localhost:5202")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazorClient");
app.UseHttpsRedirection();

// Get API's

app.MapGet("/", (PostService service) =>
{
    return new { message = "Hello World!" };
});

app.MapGet("api/posts", (PostService service) =>
{
    return service.GetPosts();
});

app.MapGet("api/posts/{id}", (PostService service, int id) =>
{
    return service.GetPost(id);
});

// Put API's

app.MapPut("api/posts/{id}/upvote", (PostService service, int id) =>
{
    return service.UpvotePost(id)
        ? service.GetPost(id)
        : null;
    
});

app.MapPut("api/posts/{id}/downvote", (PostService service, int id) =>
{
    return service.DownvotePost(id)
        ? service.GetPost(id)
        : null;
});

app.MapPut("api/posts/{postId}/comments/{commentId}/upvote", (PostService service, int postId, int commentId) =>
{
    return service.UpvoteComment(commentId)
        ? service.GetPost(postId)
        : null;
});

app.MapPut("api/posts/{postId}/comments/{commentId}/downvote", (PostService service, int postId, int commentId) =>
{
    return service.DownvoteComment(commentId)
        ? service.GetPost(postId)
        : null;
});

// Post API's

app.MapPost("api/posts", (PostService service, PostRequest postRequest) =>
{
    return service.CreatePost(postRequest);
});

app.MapPost("api/posts/{postId}/comments", (PostService service, int postId, CommentRequest commentRequest) =>
{
    return service.CreateComment(postId, commentRequest)
        ? service.GetPost(postId)
        : null;
});



app.Run();
