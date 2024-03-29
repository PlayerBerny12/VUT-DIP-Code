using DeepfakeDetectionFramework;
using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Interfaces;
using DeepfakeDetectionFramework.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

// Create logger
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Debug()
    .CreateLogger();
try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    // Enable serilog
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddCors();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("Database"),
            ob => ob.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)
        )
    );
    builder.Services.AddHttpClient();
    builder.Services.AddHealthChecks();
    builder.Services.AddHostedService<OutputConsumerService>();

    builder.Services.AddSingleton<MapperConfig>();
    builder.Services.AddScoped<IFeedbackService, FeedbackService>();
    builder.Services.AddSingleton<IFileService, FileService>();
    builder.Services.AddSingleton<IMessageService, MessageService>();
    builder.Services.AddScoped<IRequestService, RequestService>();
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
    // Build app
    WebApplication app = builder.Build();

    // Migrate latest database changes during startup
    using IServiceScope scope = app.Services.CreateScope();
    DatabaseContext dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dbContext.Database.Migrate();

    // Allow CORS
    app.UseCors(builder => 
        builder.AllowAnyOrigin()        
        .AllowAnyMethod()
        .AllowAnyHeader()
    );

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.MapHealthChecks("/ping", new HealthCheckOptions
    {
        ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}