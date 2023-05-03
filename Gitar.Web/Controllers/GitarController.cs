using Gitar.Application.DTO;
using Gitar.Domain.Contracts.Data;
using Gitar.Domain.Contracts.Infrastructure;
using Gitar.Domain.Models;
using Gitar.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gitar.Web.Controllers;

public class GitarController : Controller
{
    private readonly IRepository<GitUser, Guid> _gitUserRepository;
    private readonly IGitService _gitService;

    public GitarController(IRepository<GitUser, Guid> gitUserRepository, IGitService gitService)
    {
        _gitUserRepository = gitUserRepository;
        _gitService = gitService;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _gitUserRepository.GetAsync();

        if (!users.Any())
        {
            var neurouz = new GitUser()
            {
                Name = "neurouz",
                ImageUrl = "https://avatars.githubusercontent.com/u/40274500?v=4",
                IsActive = true,
                Blog = "http://neurouz.ddns.net",
                Followers = 18,
                Following = 33,
                GithubId = 40274500,
                ProfileUrl = "https://github.com/neurouz",
                PublicRepoCount = 9
            };

            await _gitUserRepository.CreateAsync(neurouz);
            users.Add(neurouz);
        }

        return View(users);
    }

    public async Task<IActionResult> GetUsersPartial()
    {
        var users = await _gitUserRepository.GetAsync();

        return PartialView("~/Views/Shared/Partials/_GitUsersPartial.cshtml", users);
    }

    [HttpGet]
    public async Task<IActionResult> GetUpsertForm(Guid userId)
    {
        var user = new GitUser();

        if (userId != Guid.Empty)
        {
            user = await _gitUserRepository.GetByKeyAsync(userId);
        }

        return PartialView("Upsert", user);
    }

    [HttpGet]
    public async Task<IActionResult> CheckUsername(string? username)
    {
        var result = await _gitService.CheckUsername(username);

        if (result.Success)
            return Ok();

        return BadRequest(result.Message);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(string? username)
    {
        if (string.IsNullOrEmpty(username))
            return BadRequest("Username must be specified");

        var userQuery = await _gitUserRepository.GetAsync(x => x.Name == username);

        if (userQuery is not null && userQuery.Any())
            return BadRequest($"Username '{username}' already exists in JSON");

        var githubUser = await _gitService.CheckUsername(username);

        if (githubUser.Success)
        {
            var user = githubUser.ResponseObject;
            var createResponse = await _gitUserRepository.CreateAsync(user);

            return Ok("User created successfully");
        }

        return BadRequest(githubUser.Message);
    }

    [HttpPost]
    public async Task<IActionResult> DeactivateUser(Guid userId)
    {
        var user = await _gitUserRepository.GetByKeyAsync(userId);

        if (user is not null)
        {
            user.IsActive = false;
            await _gitUserRepository.UpdateAsync(user);

            return Ok();
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetRepositoriesForUser(string? username)
    {
        var repos = await _gitService.GetRepositories(username);

        if (!repos.Success) 
            return BadRequest(repos.Message);

        var model = new GitRepositoriesViewModel()
        {
            Username = username,
            GitRepositories = repos.ResponseObject
        };

        return PartialView("~/Views/Shared/Partials/_GitUserRepositoriesPartial.cshtml", model);
    }
}
