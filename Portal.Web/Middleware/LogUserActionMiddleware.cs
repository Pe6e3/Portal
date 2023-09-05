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
        if (ShouldLogRequest(context.Request.Path.Value))
        {
            User? user = await GetUserAsync(context.User.Identity.Name);
            MyLogger logger = await CreateLogger(context, user);

            try
            {
                string userIp = await GetUserIpAddressAsync();
                string userLocation = await GetUserLocationAsync(userIp);

                logger.UserIP = userIp;
                logger.UserLocation = userLocation;

                await InsertLogAsync(logger);
            }
            catch (Exception ex)
            {
                // Обработка ошибок при получении IP и местоположения
                // Здесь можно добавить логирование ошибок
            }
        }

        await next(context);
    }

    private bool ShouldLogRequest(string path)
    {
        return !path.Contains("/img/") && !path.Contains("/lib") && !path.Contains("favicon");
    }

    private async Task<User?> GetUserAsync(string userName)
    {
        return await uow.UserRep.GetUserByLogin(userName);
    }

    private async Task<MyLogger> CreateLogger(HttpContext context, User? user)
    {
        MyLogger logger = new MyLogger();
        logger.UserIP = context.Connection.RemoteIpAddress?.ToString() ?? context.Connection.LocalIpAddress?.ToString();
        logger.UserClick = context.Request.Path.Value;
        logger.UserId = user?.Id ?? await uow.UserRep.GetDefaultUserId();
        logger.Date = DateTime.Now;
        return logger;
    }

    private async Task<string> GetUserIpAddressAsync()
    {
        using HttpClient httpClient = new HttpClient();
        string ipAddressResponse = await httpClient.GetStringAsync("http://ipwho.is/");
        Match matchIp = Regex.Match(ipAddressResponse, "\"ip\":\"(.*?)\"");
        return matchIp.Groups[1].Value;
    }

    private async Task<string> GetUserLocationAsync(string ipAddress)
    {
        using HttpClient httpClient = new HttpClient();
        string locationResponse = await httpClient.GetStringAsync("http://ipwho.is/" + ipAddress);
        Match matchCountry = Regex.Match(locationResponse, "\"country\":\"(.*?)\"");
        Match matchCity = Regex.Match(locationResponse, "\"city\":\"(.*?)\"");
        return matchCountry.Groups[1].Value + " - " + matchCity.Groups[1].Value;
    }

    private async Task InsertLogAsync(MyLogger logger)
    {
        await uow.MyLoggerRep.InsertAsync(logger);
    }

}
