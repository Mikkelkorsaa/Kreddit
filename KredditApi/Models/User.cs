namespace Models;

public class User
{
    public User() {}
    public User(string name)
    {
        Name = name;
    }
    
    public long UserId { get; set; }
    public string? Name { get; set; }
}