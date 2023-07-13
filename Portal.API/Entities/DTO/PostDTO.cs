namespace Portal.API.Entities.DTO;

public class PostDTO
{
    public int PostId { get; set; }
    public string Title { get; set; } = null!;
    public int CommentsNum { get; set; }
    public string ImgUrl { get; set; } = null!;
}
