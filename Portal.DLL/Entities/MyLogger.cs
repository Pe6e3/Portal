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

        public void FillFromHttpContext(HttpContext httpContext)
        {
          

            // Получение IP-адреса пользователя
            UserIP = httpContext.Connection.RemoteIpAddress?.ToString();

            // Получение информации о местоположении пользователя
            // (требуется дополнительная логика или сервис для определения местоположения)

            // Получение информации о действии пользователя (например, URL или действие)
            UserClick = httpContext.Request.Path;
        }
    }
}
