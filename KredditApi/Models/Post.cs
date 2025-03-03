using Models;

namespace Models;

public class Post
{
    public Post() {}
    public Post(string title, string content, User author, DateTime createdAt)
    {
        Title = title;
        Content = content;
        Comments = new List<Comment>();
        Author = author;
        CreatedAt = createdAt;
    }
    
    public long PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public User? Author { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int Votes { get; set; }
}