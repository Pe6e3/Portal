using System.ComponentModel.DataAnnotations;

namespace Portal.DAL.Entities;

public class User : BaseEntity
{
    [Display(Name = "Логин")][Required(ErrorMessage = "Не указан логин")] public string Login { get; set; } = null!;
    [Display(Name = "Пароль")][Required(ErrorMessage = "Не указан пароль")][DataType(DataType.Password)] public string? Password { get; set; } = null!;
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    public UserProfile? Profile { get; set; }
}
