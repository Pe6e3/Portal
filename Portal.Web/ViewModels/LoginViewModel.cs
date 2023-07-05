using System.ComponentModel.DataAnnotations;

namespace Portal.Web.ViewModels;

public class LoginViewModel
{

    // User
    [Required(ErrorMessage = "Не указан логин")]
    [Display(Name = "Логин")] public string Login { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Эл. почта")] public string? Email { get; set; }

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")] public string? Password { get; set; }


    [Required(ErrorMessage = "Повторите пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтверждение пароля")] public string? PasswordConfirm { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = "Дата регистрации")] public DateTime? RegistrationDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "День рождения")] public DateTime? Birthday { get; set; }



    // UserProfile 
    [Display(Name = "Имя")] public string? Firstname { get; set; }
    [Display(Name = "Фамилия")] public string? Lastname { get; set; }

    [Display(Name = "Аватар")] public string? AvatarImg { get; set; }
    [Display(Name = "Аватар")] public IFormFile? AvatarFile { get; set; }


}
