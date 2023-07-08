using System;
using System.Collections.Generic;

namespace Portal.DAL.Entities
{
    public class Chat : BaseEntity
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? ChatName { get; set; }
        public string? ChatIMG { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
