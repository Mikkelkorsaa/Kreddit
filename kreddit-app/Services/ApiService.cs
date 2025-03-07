using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

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
        string url = $"{baseAPI}posts/";
        return await http.GetFromJsonAsync<Post[]>(url);
    }

    public async Task<Post> GetPost(int id)
    {
        string url = $"{baseAPI}posts/{id}/";
        return await http.GetFromJsonAsync<Post>(url);
    }
    
    public async Task<Post> CreatePost(string title, string content, int userId)
    {
        string url = $"{baseAPI}posts/";
    
        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PostAsJsonAsync(url, new { title, content, userId });
    
        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;
    
        // Deserialize the JSON string to a Post object
        Post? newPost = JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });
    
        // Return the new post
        return newPost;
    }
    
    public async Task<Comment> CreateComment(string content, int postId, int userId)
    {
        string url = $"{baseAPI}posts/{postId}/comments";
     
        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PostAsJsonAsync(url, new { content, userId });
    
        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;
    
        // Deserialize the JSON string to a Comment object
        Comment? newComment = JsonSerializer.Deserialize<Comment>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties 
        });
    
        // Return the new comment 
        return newComment;
    }

    public async Task<Post> UpvotePost(int id)
    {
        string url = $"{baseAPI}posts/{id}/upvote/";
    
        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");
    
        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;
    
        // Deserialize the JSON string to a Post object
        Post? updatedPost = JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });
    
        // Return the updated post (vote increased)
        return updatedPost;
    }
    
    public async Task<Post> DownvotePost(int id)
    {
        string url = $"{baseAPI}posts/{id}/downvote/";
    
        // Send PUT request to API
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");
    
        // Read JSON response
        string json = msg.Content.ReadAsStringAsync().Result;
    
        // Deserialize JSON to Post object
        Post? updatedPost = JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });
    
        // Return the updated post (vote decreased)
        return updatedPost;
    }
    
    public async Task<Post> UpvoteComment(int postId, int commentId)
    {
        string url = $"{baseAPI}posts/{postId}/comments/{commentId}/upvote/";
    
        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");
        
        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;
    
        // Deserialize the JSON string to a Post object
        Post? updatedPost = JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });
    
        // Return the updated post (vote increased)
        return updatedPost;
    }
    
    public async Task<Post> DownvoteComment(int postId, int commentId)
    {
        string url = $"{baseAPI}posts/{postId}/comments/{commentId}/downvote/";
    
        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");
        
        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;
    
        // Deserialize the JSON string to a Post object
        Post? updatedPost = JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });
    
        // Return the updated post (vote increased)
        return updatedPost;
    }
}
