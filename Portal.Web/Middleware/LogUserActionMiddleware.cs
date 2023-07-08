using Microsoft.AspNetCore.Http;
using Portal.BLL;
using Portal.DAL.Entities;

public class LogUserActionMiddleware : IMiddleware
{
    private readonly UnitOfWork uow;

    public LogUserActionMiddleware(UnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

        if (!context.Request.Path.StartsWithSegments("/img"))
        {
            User? user = await uow.UserRep.GetUserByLogin(context.User.Identity.Name);
            var logger = new MyLogger();
            logger.UserIP = context.Connection.RemoteIpAddress?.ToString();
            logger.UserClick = context.Request.Path.Value;
            logger.UserId = user != null ? user.Id : await uow.UserRep.GetDefaultUserId();
            logger.Date = DateTime.UtcNow;
            await uow.MyLoggerRep.InsertAsync(logger);
        }

        // Вызов следующего middleware в конвейере запросов
        await next(context);
    }
}
