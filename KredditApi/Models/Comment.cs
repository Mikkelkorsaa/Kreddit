namespace Models;

public class Comment
{
    public Comment() {}
    
    public Comment(string content, User user)
    {
        Content = content;
        User = user;
        CreatedAt = DateTime.Now;
    }
    
    public long CommentId { get; set; }
    public string Content { get; set; }
    public User User { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Votes { get; set; }
}