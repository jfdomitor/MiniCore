using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args) =>

        Host.CreateDefaultBuilder(args)    
        .ConfigureWebHostDefaults(webBuilder =>
        {
            //webBuilder.UseKestrel();
            webBuilder.ConfigureServices((buildercontext, services) =>
            {
                services.AddRazorPages().AddRazorRuntimeCompilation();

            }).Configure(app =>
            {
                app.UseStaticFiles();
                app.UseRouting();
                app.UseEndpoints(route =>
                {
                    route.MapRazorPages();
                    //route.MapGet("/", context => context.Response.WriteAsync("Hello world"));
                });
            });
        })
        .Build().Run();
}