using CsvHelper.Configuration;
using mc_demon_api.Logic;
using mc_demon_api.Logic.Configs;
using mc_demon_api.Logic.Profiles;
using mc_demon_api.Logic.Templates;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                          });
});

builder.Services.AddControllers();

builder.Services.AddSingleton(new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));

builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();

builder.Services.AddScoped<IConfigService, ConfigService>();

builder.Services.Configure<SourceOptions>(builder.Configuration.GetSection(nameof(SourceOptions)));

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
