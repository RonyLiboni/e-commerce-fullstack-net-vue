using Rony.Store.Api;
using Rony.Store.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
            .AddInfrastructure(builder.Configuration)
            .AddServices()
            .AddRepositories()
            .AddMiddlewares()
            .AddPresentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.UseCors((builder) =>
{
    builder.SetIsOriginAllowed((_) => true);
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowCredentials();
});

app.Run();