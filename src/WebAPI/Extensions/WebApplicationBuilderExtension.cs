using System.Text.Json;
using Application;
using Infrastructure;

namespace WebAPI.Extensions;

internal static class WebApplicationBuilderExtension
{
    internal static WebApplicationBuilder ConfigureWebApplicationBuilder(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddAntiforgery();

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        });

        return builder;
    }
}
