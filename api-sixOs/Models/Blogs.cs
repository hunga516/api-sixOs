namespace api_sixOs.Models;

public class Blogs
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateOnly CreatedAt { get; set; }
    public string? Img { get; set; }
}