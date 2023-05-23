using System.Xml.Linq;

namespace Portal.DAL.Entities;

public class Post : BaseEntity
{
    public string? Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public User? CreatedBy { get; set; }

    public List<Category>? Categories { get; set; }
    public List<Comment>? Comments { get; set; }
}
