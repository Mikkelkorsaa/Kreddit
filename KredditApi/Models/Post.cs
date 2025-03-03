using Models;

namespace Models;

public class Post
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="createdAt"></param>
    /// <param name="author"></param>
    /// <param name="votes"></param>
    public Post(string title, string content, User? author, DateTime createdAt)
    {
        this.Title = title;
        this.Content = content;
        this.Comments = new List<Comment>();
        this.Author = author;
        this.CreatedAt = DateTime.Now;
    }
    
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public User? Author { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int Votes { get; set; }
}