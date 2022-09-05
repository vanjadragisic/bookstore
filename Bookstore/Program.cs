using Rhetos;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

void ConfigureRhetosHostBuilder(IServiceProvider serviceProvider, IRhetosHostBuilder rhetosHostBuilder)
{
    rhetosHostBuilder
        .ConfigureRhetosAppDefaults()
        .UseBuilderLogProviderFromHost(serviceProvider)
        .ConfigureConfiguration(cfg => cfg.MapNetCoreConfiguration(builder.Configuration));
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.CustomSchemaIds(type => type.ToString()));
builder.Host.UseNLog();

builder.Services.AddRhetosHost(ConfigureRhetosHostBuilder)
    .AddAspNetCoreIdentityUser()
    .AddHostLogging()
    .AddRestApi(o =>
    {
        o.BaseRoute = "rest";
        o.GroupNameMapper = (conceptInfo, controller, oldName) => "v1";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapRhetosDashboard();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRhetosRestApi();

app.MapControllers();

app.Run();
