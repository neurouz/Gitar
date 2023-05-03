using Gitar.Application.DTO;
using Gitar.Domain.Common;
using Gitar.Domain.Constants;
using Gitar.Domain.Contracts.Infrastructure;
using Gitar.Domain.Models;
using System.Net.Http.Json;

namespace Infrastructure.Services.Github;

public class GithubService : IGitService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public GithubService(IHttpClientFactory clientFactory)
    {
        _httpClientFactory = clientFactory;
    }

    public async Task<Response<GitUser>> CheckUsername(string? username)
    {
        if (string.IsNullOrEmpty(username))
            return Response<GitUser>.CreateError("Username must be specified");

        var client = this._httpClientFactory.CreateClient(ServiceConstants.GITHUB_SERVICE);
        var result = await client.GetAsync($"/users/{username}");

        if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            return Response<GitUser>.CreateWarning($"Github user '{username}' not found");

        if (result.IsSuccessStatusCode)
        {
            var user = await result.Content.ReadFromJsonAsync<GitUserDTO>();

            return Response<GitUser>.CreateSuccess("Username is valid", instance: user?.DomainModel);
        }

        return Response<GitUser>.CreateError(result.ReasonPhrase);
    }

    public async Task<Response<List<GitRepository>>> GetRepositories(string? username)
    {
        if (string.IsNullOrEmpty(username))
            return Response<List<GitRepository>>.CreateError("Username must be specified");

        var client = this._httpClientFactory.CreateClient(ServiceConstants.GITHUB_SERVICE);
        var result = await client.GetAsync($"/users/{username}/repos");

        if (result.IsSuccessStatusCode)
        {
            var repos = await result.Content.ReadFromJsonAsync<List<GitRepository>>();

            return Response<List<GitRepository>>.CreateSuccess(instance: repos);
        }

        return Response<List<GitRepository>>.CreateError(result.ReasonPhrase);
    }
}
