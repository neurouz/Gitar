using Gitar.Domain.Contracts.Data;
using Gitar.Domain.Contracts.Infrastructure;
using Gitar.Domain.Models;
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
                ImageUrl = "https://avatars.githubusercontent.com/u/40274500?v=4"
            };

            await _gitUserRepository.CreateAsync(neurouz);
        }


        return View(users);
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
}
