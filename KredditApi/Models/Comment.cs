using Models;

namespace Models;

public class Comment
{
    public Comment(string body, User user)
    {
        this.Body = body;
        this.User = user;
        this.CreatedAt = DateTime.Now;
    }
    string Body { get; set; }
    User User { get; set; }
    DateTime CreatedAt { get; set; }
    int Votes { get; set; }
}