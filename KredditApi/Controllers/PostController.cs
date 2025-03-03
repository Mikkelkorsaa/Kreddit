using Contexts;
using Microsoft.AspNetCore.Mvc;

namespace KredditApi.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly PostContext _context;

    public PostController(PostContext context)
    {
        _context = context;
    }
    
    // GET: api/posts
    [HttpGet]
    public IActionResult GetPosts()
    {
        return Ok();
    }

    // GET: api/posts/{id}
    [HttpGet("{id}")]
    public IActionResult GetPost(int id)
    {
        return Ok();
    }

    // PUT: api/posts/{id}/upvote
    [HttpPut("{id}/upvote")]
    public IActionResult UpvotePost(int id)
    {
        return Ok();
    }

    // PUT: api/posts/{id}/downvote
    [HttpPut("{id}/downvote")]
    public IActionResult DownvotePost(int id)
    {
        return Ok();
    }

    // PUT: api/posts/{postId}/comments/{commentId}/upvote
    [HttpPut("{postId}/comments/{commentId}/upvote")]
    public IActionResult UpvoteComment(int postId, int commentId)
    {
        return Ok();
    }

    // PUT: api/posts/{postId}/comments/{commentId}/downvote
    [HttpPut("{postId}/comments/{commentId}/downvote")]
    public IActionResult DownvoteComment(int postId, int commentId)
    {
        return Ok();
    }

    // POST: api/posts
    [HttpPost]
    public IActionResult CreatePost()
    {
        return Ok();
    }

    // POST: api/posts/{id}/comments
    [HttpPost("{id}/comments")]
    public IActionResult CreateComment(int id)
    {
        return Ok();
    }
}