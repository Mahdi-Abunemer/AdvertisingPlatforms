using AdvertisingPlatforms.Application.Interfaces;
using AdvertisingPlatforms.Application.Services;
using AdvertisingPlatforms.Infrastructure.Repositories;
using AdvertisingPlatforms.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddSingleton<IAdvertisingPlatformRepository, InMemoryAdvertisingPlatformRepository>();

builder.Services.AddScoped<FileUploadService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseExceptionHadlingMiddleware();                                                                                                                
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();