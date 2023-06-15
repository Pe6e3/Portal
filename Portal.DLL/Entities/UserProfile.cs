namespace Portal.DAL.Entities;

public class UserProfile :BaseEntity
{
    public int UserId { get; set; }
    public User? User { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public DateTime? Birthday { get; set; }
    public string? AvatarImg { get; set; }


}