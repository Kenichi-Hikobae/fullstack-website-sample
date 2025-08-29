using WebsiteServerApp;
using WebsiteServerApp.BusinessServices;
using WebsiteServerApp.DataAccess;
using WebsiteServerApp.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// Dependency Injection
builder.Services.AddSingleton<IAppSettings, AppSettings>();
builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        using (var scope = builder.Services.BuildServiceProvider().CreateScope())
        {
            var appSettings = scope.ServiceProvider.GetRequiredService<AppSettings>();

            var webBaseUrl = appSettings.GetSetting(AppSettingType.WebBaseURL);

            policy.WithOrigins(webBaseUrl)
              .AllowAnyHeader()
              .AllowAnyMethod();
        }
    });
});

/// Default Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();
