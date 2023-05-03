using Gitar.Application.Configuration;
using Gitar.Domain.Constants;
using Gitar.Domain.Contracts.Data;
using Gitar.Domain.Contracts.Infrastructure;
using Gitar.Domain.Models;
using Infrastructure.Data.Json.Repositories;
using Infrastructure.Services.Github;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<ApiServiceConfiguration>(builder.Configuration.GetSection(ApiServiceConfiguration.CONFIGNAME));
builder.Services.Configure<DataSourceConfiguration>(builder.Configuration.GetSection(DataSourceConfiguration.CONFIGNAME));

builder.Services.AddSingleton<IRepository<GitUser, Guid>, GitUserJsonRepository>();
builder.Services.AddScoped<IGitService, GithubService>();

builder.Services.AddHttpClient(ServiceConstants.GITHUB_SERVICE, c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("GithubApi:BaseAddress"));
    c.DefaultRequestHeaders.Accept.Clear();
    c.DefaultRequestHeaders.Authorization = 
        new AuthenticationHeaderValue("Bearer", builder.Configuration.GetValue<string>("GithubApi:AccessToken"));
    c.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
});

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
