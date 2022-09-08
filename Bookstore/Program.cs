using Microsoft.OpenApi.Models;
using Rhetos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
    ;

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o => o.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("rhetos", new OpenApiInfo { Title = "Rhetos REST API", Version = "v1" });
});

builder.Services
    .AddRhetosHost((serviceProvider, rhetosHostBuilder) =>
    {
        rhetosHostBuilder
            .ConfigureRhetosAppDefaults()
            .UseBuilderLogProviderFromHost(serviceProvider)

            .ConfigureConfiguration(cfg => cfg.MapNetCoreConfiguration(builder.Configuration));
    })
    .AddRestApi(o =>
    {
        o.BaseRoute = "rhetos";
        o.GroupNameMapper = (conceptInfo, controller, oldName) => "rhetos";
    })
    //.AddDashboard()
    .AddAspNetCoreIdentityUser()
    .AddHostLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/rhetos/swagger.json", "Rhetos REST API");
    });
}
app.UseRhetosRestApi();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();