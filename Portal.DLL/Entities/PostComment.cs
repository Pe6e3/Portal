using Microsoft.EntityFrameworkCore;

namespace Portal.DAL.Entities;

public class PostComment :BaseEntity
{
    public int PostId { get; set; }
    public int CommentId { get; set; }
    public Post? Post { get; set; }
    public Comment? Comment { get; set; }
}
