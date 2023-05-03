using Gitar.Domain.Models;

namespace Gitar.Web.ViewModels;

public class GitRepositoriesViewModel
{
    public string? Username { get; set; }
    public List<GitRepository>? GitRepositories { get; set; }
}
