using Contexts;
using shared.Model;
using KredditApi.Models;
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
        var posts = _context.Posts
            .OrderBy(p => p.Id)
            .ToList();
        return Ok(posts);
    }

    // GET: api/posts/{id}
    [HttpGet("{id}")]
    public IActionResult GetPost(int id)
    {
        var post = _context.Posts
            .Include(p => p.User) 
            .Include(p => p.Comments)
                .ThenInclude(c => c.User)
            .FirstOrDefault(p => p.Id == id);
        
        if (post != null)
        {
            post.Comments = post.Comments
                .OrderBy(c => c.Id)
                .ToList();
        }
        
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
        var post = _context.Posts.FirstOrDefault(p => p.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        post.Upvotes++;
        _context.SaveChanges();

        return Ok(post);
    }

    // PUT: api/posts/{id}/downvote
    [HttpPut("{id}/downvote")]
    public IActionResult DownvotePost(int id)
    {
        var post = _context.Posts.FirstOrDefault(p => p.Id == id);
        if (post == null)
        {
            return NotFound();
        }

        post.Downvotes++;
        _context.SaveChanges();

        return Ok(post);
    }

    // PUT: api/posts/{postId}/comments/{commentId}/upvote
    [HttpPut("{postId}/comments/{commentId}/upvote")]
    public IActionResult UpvoteComment(int postId, int commentId)
    {
        var comment = _context.Comments
            .Include(c => c.User)
            .FirstOrDefault(c => c.Id == commentId);
        
        if (comment == null)
        {
            return NotFound("Comment not found");
        }

        comment.Upvotes++;
        _context.SaveChanges();

        // Include logic to fetch the full Post and return the updated Post
        var updatedPost = _context.Posts
            .Include(p => p.User) 
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefault(p => p.Id == postId);

        if (updatedPost != null)
        {
            updatedPost.Comments = updatedPost.Comments
                .OrderBy(c => c.Id)
                .ToList();
        }


        return Ok(updatedPost);
    }

    // PUT: api/posts/{postId}/comments/{commentId}/downvote
    [HttpPut("{postId}/comments/{commentId}/downvote")]
    public IActionResult DownvoteComment(int postId, int commentId)
    {
        var comment = _context.Comments
            .Include(c => c.User)
            .FirstOrDefault(c => c.Id == commentId);
        
        if (comment == null)
        {
            return NotFound("Comment not found");
        }

        comment.Downvotes++;
        _context.SaveChanges();

        // Include logic to fetch the full Post and return the updated Post
        var updatedPost = _context.Posts
            .Include(p => p.User) 
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefault(p => p.Id == postId);

        if (updatedPost != null)
        {
            updatedPost.Comments = updatedPost.Comments
                .OrderBy(c => c.Id)
                .ToList();
        }


        return Ok(updatedPost);
    }

    // POST: api/posts
    [HttpPost]
    public IActionResult CreatePost([FromBody] PostRequest postRequest)
    {
        if (postRequest == null || string.IsNullOrWhiteSpace(postRequest.Title) || string.IsNullOrWhiteSpace(postRequest.Content))
        {
            return BadRequest("Post data is invalid");
        }

        var user = _context.Users.FirstOrDefault(u => u.Id == postRequest.UserId);
        if (user == null)
        {
            return NotFound($"User with ID {postRequest.UserId} not found");
        }

        var newPost = new Post(user, postRequest.Title, postRequest.Content);
        _context.Posts.Add(newPost);
        _context.SaveChanges();
    
        return CreatedAtAction(nameof(GetPost), new { id = newPost.Id }, newPost);
    }

    // POST: api/posts/{id}/comments
    [HttpPost("{postId}/comments")]
    public IActionResult CreateComment(int postId, [FromBody] CommentRequest commentRequest)
    {
        if (commentRequest == null || string.IsNullOrWhiteSpace(commentRequest.Content))
        {
            return BadRequest("Comment content cannot be null or empty");
        }

        var post = _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefault(p => p.Id == postId);

        if (post == null)
        {
            return NotFound($"Post with ID {postId} not found");
        }

        var newComment = new Comment
        {
            Content = commentRequest.Content,
            Upvotes = 0,
            Downvotes = 0,
            User = _context.Users.FirstOrDefault(u => u.Id == commentRequest.UserId)
        };

        if (newComment.User == null)
        {
            return BadRequest($"User with ID {commentRequest.UserId} not found");
        }

        post.Comments.Add(newComment);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, newComment);
    }
}