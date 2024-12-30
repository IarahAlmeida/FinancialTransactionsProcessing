using WebAPI.Endpoints;

namespace WebAPI.Extensions;

internal static class WebApplicationExtension
{
    internal static WebApplication ConfigureWebApplication(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapDataAnalyzerEndpoints();
        app.MapTransactionEndpoints();

        app.UseAntiforgery();

        return app;
    }
}
