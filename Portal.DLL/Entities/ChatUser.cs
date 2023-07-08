using System;
using System.Collections.Generic;

namespace Portal.DAL.Entities
{
    public class ChatUser : BaseEntity
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
