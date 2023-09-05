using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.Differencing;
using Portal.BLL;
using Portal.DAL.Entities;
using System.Net;
using System.Text.RegularExpressions;

public class LogUserActionMiddleware : IMiddleware
{
    private readonly UnitOfWork uow;

    public LogUserActionMiddleware(UnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

        if (!context.Request.Path.Value.Contains("/img/") && !context.Request.Path.Value.Contains("/lib") && !context.Request.Path.Value.Contains("favicon"))
        {
            User? user = await uow.UserRep.GetUserByLogin(context.User.Identity.Name);
            var logger = new MyLogger();
            logger.UserIP = context.Connection.RemoteIpAddress?.ToString() ?? context.Connection.LocalIpAddress?.ToString();
            logger.UserClick = context.Request.Path.Value;
            logger.UserId = user != null ? user.Id : await uow.UserRep.GetDefaultUserId();
            logger.Date = DateTime.Now;

            string lineApiIp = "";
            string lineApiLocation = "";
            string regexIp = "\"ip\":\"(.*?)\"";
            string regexCountry = "\"country\":\"(.*?)\"";
            string regexCity = "\"city\":\"(.*?)\"";



            using (WebClient wc = new WebClient())
            {
                lineApiIp = wc.DownloadString("http://ipwho.is/");
                Match matchIp = Regex.Match(lineApiIp, regexIp);
                logger.UserIP = matchIp.Groups[1].Value;

                lineApiLocation = wc.DownloadString("http://ipwho.is/" + logger.UserIP);
            }

            Match matchCountry = Regex.Match(lineApiLocation, regexCountry);
            Match matchCity = Regex.Match(lineApiLocation, regexCity);
            logger.UserLocation = matchCountry.Groups[1].Value + " - " + matchCity.Groups[1].Value;
            await uow.MyLoggerRep.InsertAsync(logger);
        }

        // Вызов следующего middleware в конвейере запросов
        await next(context);
    }
}
