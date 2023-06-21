using System.ComponentModel.DataAnnotations;

namespace Portal.Web.ViewModels;

public class LoginViewModel
{

// User
	[Display(Name = "Логин")]
	[Required(ErrorMessage = "Не указан логин")]
	public string Login { get; set; } = null!;

	[Required(ErrorMessage = "Не указан Email")]
	[DataType(DataType.EmailAddress)]
	[Display(Name = "Эл. почта")]
	public string? Email { get; set; }

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
	[Display(Name = "Пароль")]
    public string? Password { get; set; }


    [Required(ErrorMessage = "Повторите пароль")]
    [DataType(DataType.Password)]
	[Display(Name = "Подтверждение пароля")]
    public string? PasswordConfirm { get; set; }

	public DateTime? RegistrationDate { get; set; }




	// UserProfile 
	[Display(Name = "Имя")]
	public string? Firstname { get; set; }
	[Display(Name = "Фамилия")]
	public string? Lastname { get; set; }

	public string? AvatarImg { get; set; }

	[Display(Name = "Ваш аватар")]
	public IFormFile? AvatarFile { get; set; }

	[Display(Name = "День рождения")]
	public DateTime? Birthday { get; set; }

}
