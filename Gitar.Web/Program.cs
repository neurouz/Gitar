using Gitar.Application.Configuration;
using Gitar.Domain.Contracts.Data;
using Gitar.Domain.Models;
using Infrastructure.Data.Json.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ApiServiceConfiguration>(builder.Configuration.GetSection("GithubApi"));
builder.Services.Configure<DataSourceConfiguration>(builder.Configuration.GetSection("DataSource"));

builder.Services.AddScoped<IRepository<GitUser, int>, GitUserJsonRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
