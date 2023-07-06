using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;

namespace Portal.DAL.Entities
{
    public class MyLogger : BaseEntity
    {
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string? UserIP { get; set; }
        public string? UserLocation { get; set; }
        public string? UserClick { get; set; }
        public virtual User? User { get; set; }

        
    }
}
