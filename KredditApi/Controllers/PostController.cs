using Contexts;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var posts = _context.Posts.ToList();
        return Ok(posts);
    }

    // GET: api/posts/{id}
    [HttpGet("{id}")]
    public IActionResult GetPost(int id)
    {
        var post = _context.Posts.FirstOrDefault(p => p.PostId == id);
        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    // PUT: api/posts/{id}/upvote
    [HttpPut("{id}/upvote")]
    public IActionResult UpvotePost(int id)
    {
        var post = _context.Posts.FirstOrDefault(p => p.PostId == id);
        if (post == null)
        {
            return NotFound();
        }

        post.Votes++;
        _context.SaveChanges();

        return Ok(post);
    }

    // PUT: api/posts/{id}/downvote
    [HttpPut("{id}/downvote")]
    public IActionResult DownvotePost(int id)
    {
        var post = _context.Posts.FirstOrDefault(p => p.PostId == id);
        if (post == null)
        {
            return NotFound();
        }

        post.Votes--;
        _context.SaveChanges();

        return Ok(post);
    }

    // PUT: api/posts/{postId}/comments/{commentId}/upvote
    [HttpPut("{postId}/comments/{commentId}/upvote")]
    public IActionResult UpvoteComment(int postId, int commentId)
    {
        var post = _context.Posts.FirstOrDefault(p => p.PostId == postId);
        if (post == null)
        {
            return NotFound("Post not found");
        }

        var comment = post.Comments.FirstOrDefault(c => c.CommentId == commentId);
        if (comment == null)
        {
            return NotFound("Comment not found");
        }

        comment.Votes++;
        _context.SaveChanges();

        return Ok(comment);
    }

    // PUT: api/posts/{postId}/comments/{commentId}/downvote
    [HttpPut("{postId}/comments/{commentId}/downvote")]
    public IActionResult DownvoteComment(int postId, int commentId)
    {
        var post = _context.Posts.FirstOrDefault(p => p.PostId == postId);
        if (post == null)
        {
            return NotFound("Post not found");
        }

        var comment = post.Comments.FirstOrDefault(c => c.CommentId == commentId);
        if (comment == null)
        {
            return NotFound("Comment not found");
        }

        comment.Votes--;
        _context.SaveChanges();

        return Ok(comment);
    }

    // POST: api/posts
    [HttpPost]
    public IActionResult CreatePost([FromBody] Post post)
    {
        if (post == null)
        {
            return BadRequest("Post cannot be null");
        }

        var newPost = new Post(post.Title, post.Content);
        _context.Posts.Add(newPost);
        _context.SaveChanges();
    
        return CreatedAtAction(nameof(GetPost), new { id = post.PostId }, newPost);
    }

    // POST: api/posts/{id}/comments
    [HttpPost("{id}/comments")]
    public IActionResult CreateComment(int id, [FromBody] Comment comment)
    {
        var post = _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefault(p => p.PostId == id);
    
        if (post == null)
        {
            return NotFound($"Post with ID {id} not found");
        }

        if (comment == null)
        {
            return BadRequest("Comment cannot be null");
        }

        var newComment = new Comment(comment.Content);
        post.Comments.Add(newComment);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetPost), new { id = post.PostId }, newComment);
    }
}