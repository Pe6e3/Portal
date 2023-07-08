using Portal.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Portal.Web.ViewModels;

public class ChatProfileViewModel : BaseEntity
{
    [Display(Name = "Имя")] public string? Firstname { get; set; }
    [Display(Name = "Фамилия")] public string? Lastname { get; set; }
    [Display(Name = "Дата регистрации")] public DateTime? RegistrationDate { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    [Display(Name = "Дата рождения")] public DateTime? Birthday { get; set; }
    [Display(Name = "Эл. почта")] public string? Email { get; set; }
    [Display(Name = "Аватар")] public string? AvatarImg { get; set; }

    [Display(Name = "Роль")] public Role? Role { get; set; }
    public int RoleId { get; set; }
 


}
