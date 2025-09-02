using WebsiteServerApp;
using WebsiteServerApp.BusinessServices;
using WebsiteServerApp.DataAccess;
using WebsiteServerApp.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Configure(context.Configuration.GetSection("Kestrel"));
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// Dependency Injection
builder.Services.AddSingleton<IAppSettings, AppSettings>();
builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddTransient<ErrorHandlerMiddleware>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        using (var scope = builder.Services.BuildServiceProvider().CreateScope())
        {
            var appSettings = scope.ServiceProvider.GetRequiredService<IAppSettings>();

            var webBaseUrl = appSettings.GetSetting(AppSettingType.WebBaseURL);

            policy.WithOrigins(webBaseUrl)
            //policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
        }
    });
});

/// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddOpenApiDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.UseOpenApi();
app.UseSwaggerUI();

app.Run();
