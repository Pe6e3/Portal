using Portal.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Web.ViewModels;

public class PostViewModel 
{
    [StringLength(30, ErrorMessage = "Короткая ссылка должна быть не более 30 символов")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Короткая ссылка должна содержать только латинские буквы и цифры.")]
    [Display(Name = "Короткая ссылка")] public string? Slug { get; set; }
    [Display(Name = "Дата создания")] public DateTime CreatedAt { get; set; }
    [Display(Name = "Кем создано")] public User? CreatedBy { get; set; } = default(User?);
    [Display(Name = "ID поста")] public int PostId { get; set; }
    [Display(Name = "ID Контента")] public int ContentId { get; set; }

    [Display(Name = "Название поста")][StringLength(60, ErrorMessage = "Длина поста должна быть не более 60 символов")] public string? Title { get; set; }
    [Display(Name = "Текст поста")] public string? PostBody { get; set; }
    [Display(Name = "Изображение поста")] public string? PostImage { get; set; }
    [Display(Name = "Количество комментариев")] public int CommentsNum { get; set; }
    [Display(Name = "Комментарии разрешены")] public bool CommentsClosed { get; set; }
    [Display(Name = "Комментарии")] public List<Comment>? Comments { get; set; }
    [Display(Name = "Видео")] public string? PostVideo { get; set; }
    [Display(Name = "Категории")] public int[]? CategoriesId { get; set; }

    [NotMapped]
    [Display(Name = "Изображение")] public IFormFile? ImageFile { get; set; }
    [Display(Name = "Категория")] public Category? Category { get; set; }

}
