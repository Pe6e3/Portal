using Microsoft.EntityFrameworkCore;

namespace Portal.DAL.Entities;

[Keyless]
public class PostCategory
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public Post? Post { get; set; }
    public Category? Category { get; set; }
}
