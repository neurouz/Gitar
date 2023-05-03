using AutoMapper;
using Gitar.Application.DTO;
using Gitar.Domain.Common;
using Gitar.Domain.Constants;
using Gitar.Domain.Contracts.Infrastructure;
using Gitar.Domain.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace Infrastructure.Services.Github;

public class GithubService : IGitService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMapper _mapper;
    public GithubService(IHttpClientFactory clientFactory, IMapper mapper)
    {
        _httpClientFactory = clientFactory;
        _mapper = mapper;
    }

    public async Task<Response<GitUser>> CheckUsername(string? username)
    {
        try
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

                return Response<GitUser>.CreateSuccess(instance: user?.DomainModel);
            }

            return Response<GitUser>.CreateError(result.ReasonPhrase);

        }
        catch (Exception ex) 
        {
            int i = 0;
        }
        return null;
    }

    public Task<List<GitRepository>> GetRepositories(string? username)
    {
        throw new NotImplementedException();
    }
}
