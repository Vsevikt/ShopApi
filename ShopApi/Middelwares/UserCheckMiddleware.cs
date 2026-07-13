using Microsoft.AspNetCore.Http;
using ShopDomain.Models;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopApi.Middlewares
{
    //public class UserCheckMiddleware(RequestDelegate _next)
    //{
    //    //private readonly RequestDelegate _next;

    //    //public UserCheckMiddleware(RequestDelegate next)
    //    //{
    //    //    _next = next;
    //    //}

    //    public async Task InvokeAsync(HttpContext context)
    //    {
    //        //if (context.Request.Method == "POST" && context.Request.Path == "/api/user/register")
    //        //{
    //        //    await _next(context);
    //        //    return;
    //        //}

    //        //context.Request.EnableBuffering();

    //        //var user = await JsonSerializer.DeserializeAsync<User>(context.Request.Body);
    //        //context.Request.Body.Position = 0;

    //        //if (user?.Id == 1 && user.Login == "admin")
    //        //{
    //        //    await _next(context);
    //        //    return;
    //        //}

    //        //context.Response.StatusCode = 400;
    //        //await context.Response.WriteAsJsonAsync(new { message = "No authorization" });

    //        context.Request.EnableBuffering();

    //        var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
    //        context.Request.Body.Position = 0;

    //        var user = JsonSerializer.Deserialize<User>(body);

    //        if (user?.Id == 1 && user.Login == "admin")
    //        {
    //            await _next(context);
    //            return;
    //        }

    //        context.Response.StatusCode = 400;
    //        await context.Response.WriteAsync("Forbidden");
    //    }
    //}
}