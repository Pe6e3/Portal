namespace Portal.DAL.Entities;

public class Comment :BaseEntity
{
    public string? TextComment { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int LikeCount { get; set; }
}
