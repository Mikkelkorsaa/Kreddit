namespace Models;

public class Comment
{
    public Comment() {}
    
    public Comment(string content)
    {
        Content = content;
        User = new User("Anonymous");
        CreatedAt = DateTime.Now;
    }
    
    public long CommentId { get; set; }
    public string? Content { get; set; }
    public User? User { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Votes { get; set; }
}