using Microsoft.EntityFrameworkCore;

namespace Portal.DAL.Entities;

[Keyless]
public class PostComment
{
    public int Id { get; set; }
    public int CommentId { get; set; }
    public Post? Post { get; set; }
    public Comment? Comment { get; set; }
}
