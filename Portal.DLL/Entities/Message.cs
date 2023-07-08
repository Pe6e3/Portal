using System;

namespace Portal.DAL.Entities;

public class Message : BaseEntity
{
    public int ChatId { get; set; }
    public string? TextMessage { get; set; }
    public DateTime? SentAt { get; set; }
    public bool WasEdited { get; set; }
    public int LikesCount { get; set; }
    public int RepliedToId { get; set; }
}
