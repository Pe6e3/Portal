using System;
using System.Collections.Generic;

namespace Portal.DAL.Entities
{
    public class Chat : BaseEntity
    {
        public DateTime? CreatedAt { get; set; }
        public string? ChatName { get; set; }
        public string? ChatIMG { get; set; }
        public virtual List<User>? Users { get; set; }
    }
}
