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
	public string? Email { get; set; }

	[Required(ErrorMessage = "Не указан пароль")]
	[DataType(DataType.Password)]




// UserProfile 
	public string? Password { get; set; }
	[Display(Name = "Имя")]
	public string? Firstname { get; set; }
	[Display(Name = "Фамилия")]
	public string? Lastname { get; set; }

}
