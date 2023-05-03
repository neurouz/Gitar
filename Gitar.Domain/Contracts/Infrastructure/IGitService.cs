using Gitar.Domain.Common;
using Gitar.Domain.Models;

namespace Gitar.Domain.Contracts.Infrastructure;

public interface IGitService
{
    Task<Response<GitUser>> CheckUsername(string? username);
    Task<List<GitRepository>> GetRepositories(string? username);
}
