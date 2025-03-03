using Models;
using Microsoft.EntityFrameworkCore;

namespace Contexts;

public class PostContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public string DbPath { get; }

    public PostContext()
    {
        DbPath = "bin/posts.db";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().ToTable("Posts");
    }
    
}