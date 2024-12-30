using WebAPI.Extensions;

namespace WebAPI;

internal static class Program
{
    internal static void Main(string[] args)
    {
        var builder = WebApplication
            .CreateBuilder(args)
            .ConfigureWebApplicationBuilder();

        var app = builder
            .Build()
            .ConfigureWebApplication();

        app.Run();
    }
}
