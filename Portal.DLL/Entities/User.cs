using System.ComponentModel.DataAnnotations;

namespace Portal.DAL.Entities;

public class User : BaseEntity
{
    [Display(Name = "Логин")]
    [Required(ErrorMessage = "Не указан логин")]
    public string Login { get; set; } = null!;
    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string? Password { get; set; } = null!;
    public string? Email { get; set; }
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    public UserProfile? Profile { get; set; }
}
