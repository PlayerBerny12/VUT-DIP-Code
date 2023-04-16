using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Interfaces;
using DeepfakeDetectionFramework.Services;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

builder.Services.AddSingleton<MapperConfig>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<IMessageService, MessageService>();
builder.Services.AddScoped<IRequestService, RequestService>();

// Build app
WebApplication app = builder.Build();

// Migrate latest database changes during startup
using IServiceScope scope = app.Services.CreateScope();
DatabaseContext dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
dbContext.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
