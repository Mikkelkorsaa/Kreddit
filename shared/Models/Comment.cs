namespace shared.Model;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public User User { get; set; }
    public Comment(User user = null, string content = "", int upvotes = 0, int downvotes = 0)
    {
        Content = content;
        Upvotes = upvotes;
        Downvotes = downvotes;
        User = user;
    }
    public Comment() {
        Id = 0;
        Content = "";
        Upvotes = 0;
        Downvotes = 0;
    }
}
