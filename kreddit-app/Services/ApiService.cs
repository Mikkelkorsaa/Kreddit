using System.Net.Http.Json;
using System.Text.Json;
using KredditApi.Models;
using shared.Model;

namespace kreddit_app.Data;

public class ApiService
{
    private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";

    public ApiService(HttpClient http, IConfiguration configuration)
    {
        this.http = http;
        this.configuration = configuration;
        this.baseAPI = configuration["base_api"];
    }

    public async Task<Post[]> GetPosts()
    {
        string url = $"{baseAPI}posts";
        return await http.GetFromJsonAsync<Post[]>(url);
    }

    public async Task<Post> GetPost(int id)
    {
        string url = $"{baseAPI}posts/{id}";
        return await http.GetFromJsonAsync<Post>(url);
    }
    
    public async Task<Post> CreatePost(string title, string content, int userId)
    {
        string url = $"{baseAPI}posts";

        PostRequest postRequest = new PostRequest
        {
            Title = title,
            Content = content,
            UserId = userId
        };

        HttpResponseMessage response = await http.PostAsJsonAsync(url, postRequest);

        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<Post>();
        
    }
    
    public async Task<Comment> CreateComment(string content, int postId, int userId)
    {
        string url = $"{baseAPI}posts/{postId}/comments";
        
        CommentRequest commentRequest = new CommentRequest
        {
            Content = content,
            UserId = userId
        };
        
        HttpResponseMessage response = await http.PostAsJsonAsync(url, commentRequest);
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<Comment>();
    }

    public async Task<Post> UpvotePost(int id)
    {
        string url = $"{baseAPI}posts/{id}/upvote";
    
        var response = await http.PutAsJsonAsync(url, "");
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<Post>();
    }
    
    public async Task<Post> DownvotePost(int id)
    {
        string url = $"{baseAPI}posts/{id}/downvote";
    
        var response = await http.PutAsJsonAsync(url, "");
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<Post>();
    }
    
    public async Task<Post> UpvoteComment(int postId, int commentId)
    {
        string url = $"{baseAPI}posts/{postId}/comments/{commentId}/upvote";
    
        var response = await http.PutAsJsonAsync(url, "");
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<Post>();
    }
    
    public async Task<Post> DownvoteComment(int postId, int commentId)
    {
        string url = $"{baseAPI}posts/{postId}/comments/{commentId}/downvote";
    
        var response = await http.PutAsJsonAsync(url, "");
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<Post>();
    }
}
