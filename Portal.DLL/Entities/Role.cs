using Portal.DAL.Enum;

namespace Portal.DAL.Entities;

public class Role : BaseEntity
{
    public RoleName RoleName { get; set; }
}
