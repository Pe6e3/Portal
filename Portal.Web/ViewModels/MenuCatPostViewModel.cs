using Portal.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Portal.Web.ViewModels;

public class MenuCatPostViewModel : BaseEntity
{
    [StringLength(25, ErrorMessage = "Длина имени пункта меню не должна быть больше 25 символов")]
    [Display(Name = "Имя пункта меню")] public string? Name { get; set; }
    [Display(Name = "Приоритет расположения меню слева направо (сверху вниз)")] public int Position { get; set; } = 1;
    [Display(Name = "Категория, на которую ссылается пункт меню")] public IReadOnlyList<Category>? Categories { get; set; }
    [Display(Name = "Пост, на который ссылается пункт меню")] public IReadOnlyList<Post>? Posts { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9\-._~:/?#\[\]@!$&'()*+,;=]+$", ErrorMessage = "Ссылка должна содержать только допустимые символы для URL")]
    [Display(Name = "url на который будет ссылаться пункт меню")] public string? Url { get; set; }

    public virtual PostContent? PostContent { get; set; }
    public string? SelectedOption { get; set; }
    public int MenuId { get; set; }
}
