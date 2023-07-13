using Portal.DAL.Entities;

namespace Portal.Web.Areas.Admin.Controllers.ViewModels;

public class CategoryViewModel :BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public string? CategoryImage { get; set; }
    public IFormFile CategoryImageFile { get; set; }
}
