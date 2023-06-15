namespace Portal.DAL.Entities;

public class User : BaseEntity
{
    public string Login { get; set; } = null!;
    public string? Password { get; set; } = null!;
    public string? Email { get; set; }
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    public UserProfile? Profile { get; set; }
}
