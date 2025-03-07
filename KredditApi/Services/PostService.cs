using shared.Model;
using KredditApi.Models;
using KredditApi.Contexts;
using Microsoft.EntityFrameworkCore;

namespace KredditApi.Services;

public class PostService
{
    private readonly PostContext _context;

    public PostService(PostContext context)
    {
        _context = context;
    }

    // Retrieve all posts
    public IEnumerable<Post>? GetPosts()
    {
        var posts = _context.Posts
            .Include(p => p.User)
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .OrderBy(p => p.Id)
            .ToList();
        return posts.Count != 0 
            ? posts
            : null; 
    }

    // Retrieve a single post by ID
    public Post? GetPost(int id)
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
        
        return post;
    }

    // Create a new post
    public Post CreatePost(PostRequest postRequest)
    {
        var newPost = new Post
        (
            _context.Users.FirstOrDefault(u => u.Id == postRequest.UserId)!,
            postRequest.Title,
            postRequest.Content
        );

        _context.Posts.Add(newPost);
        _context.SaveChanges();
        return newPost;
    }

    // Upvote a post
    public bool UpvotePost(int id)
    {
        var post = _context.Posts.FirstOrDefault(p => p.Id == id);
        if (post == null) return false;

        post.Upvotes++;
        _context.SaveChanges();
        return true;
    }

    // Downvote a post
    public bool DownvotePost(int id)
    {
        var post = _context.Posts.FirstOrDefault(p => p.Id == id);
        if (post == null) return false;

        post.Downvotes++;
        _context.SaveChanges();
        return true;
    }

    // Create a new comment on a post
    public bool CreateComment(int postId, CommentRequest commentRequest)
    {
        var post = _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefault(p => p.Id == postId);
            
        if (post == null) return false;

        var newComment = new Comment
        (
            _context.Users.FirstOrDefault(u => u.Id == commentRequest.UserId)!,
            commentRequest.Content
        );

        post.Comments.Add(newComment);
        _context.SaveChanges();
        return true;
    }

    // Upvote a comment
    public bool UpvoteComment(int commentId)
    {
        var comment = _context.Comments
            .FirstOrDefault(c => c.Id == commentId);
        if (comment == null) return false;

        comment.Upvotes++;
        _context.SaveChanges();
        return true;
    }

    // Downvote a comment
    public bool DownvoteComment(int commentId)
    {
        var comment = _context.Comments
            .FirstOrDefault(c => c.Id == commentId);
        if (comment == null) return false;

        comment.Downvotes++;
        _context.SaveChanges();
        return true;
    }
}