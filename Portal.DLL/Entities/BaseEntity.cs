using System.ComponentModel.DataAnnotations;

namespace Portal.DAL.Entities;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
