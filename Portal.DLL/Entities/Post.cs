namespace Portal.DAL.Entities;

public class Post : BaseEntity
{
    public string? Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public User? CreatedBy { get; set; }
    public int? CategoryId { get; set; }

    public List<Category>? Categories { get; set; }
    public virtual List<Comment>? Comments { get; set; }
    public PostContent? Content { get; set; }
}
