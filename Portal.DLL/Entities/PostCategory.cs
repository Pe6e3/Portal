using Microsoft.EntityFrameworkCore;

namespace Portal.DAL.Entities;

public class PostCategory : BaseEntity
{
    public int PostId { get; set; }
    public int CategoryId { get; set; }
    public Post? Post { get; set; }
    public Category? Category { get; set; }
}
