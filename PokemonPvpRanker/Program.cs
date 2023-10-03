using Microsoft.OpenApi.Models;
using PokemonPvpRanker.Configs;
using PokemonPvpRanker.Extensions;
using PokemonPvpRanker.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

PvPokeConfig.GreatLeagueJson = builder.Configuration["PvPoke:GreatLeagueJson"]!;
PvPokeConfig.UltraLeagueJson = builder.Configuration["PvPoke:UltraLeagueJson"]!;

try
{
    StartApplication();
}
catch (Exception ex)
{
    Console.WriteLine($"The application failed to start correctly:\n\n {ex.GetExceptionMessage()}");
}

void StartApplication()
{
    ConfigureBuilder(builder);

    ConfigureServices(builder.Services);

    Configure(builder.Build());
}

void ConfigureBuilder(WebApplicationBuilder builder)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "allOpen",
                        policy =>
                        {
                            policy.AllowAnyOrigin();
                            policy.AllowAnyHeader();
                            policy.AllowAnyMethod();
                        });
    });
}

void ConfigureServices(IServiceCollection services)
{
    services.AddApiVersioning(o =>
    {
        o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
        o.ReportApiVersions = true;
    });

    services.AddVersionedApiExplorer(o =>
    {
        o.GroupNameFormat = "'v'VVV";
        o.SubstituteApiVersionInUrl = true;
    });

    services.AddControllers();

    services.AddEndpointsApiExplorer();

    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "PokemonPvpRanker", Version = "v1" });
    });

    services.RegisterClasses();

    services.AddHttpClient();
}

void Configure(WebApplication app)
{
    app.UseCors("allOpen");

    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.DocExpansion(DocExpansion.None);
    });

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
