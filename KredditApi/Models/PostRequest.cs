namespace KredditApi.Models;

public class PostRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
}