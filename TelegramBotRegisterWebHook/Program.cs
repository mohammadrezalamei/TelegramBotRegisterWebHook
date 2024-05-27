using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TelegramBotRegisterWebHook
{
    public class Program : Object
    {
        public Program()
            : base()
        {
            
        }

        public static void Main(string[] args)
        {
            Microsoft.AspNetCore.Builder.WebApplicationBuilder builder = 
                Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
            builder.Services.AddHttpContextAccessor();

            Microsoft.AspNetCore.Builder.WebApplication app = 
                builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
