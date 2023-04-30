namespace Gitar.Application.Configuration;

public class ApiServiceConfiguration
{
    public static string CONFIGNAME = "GithubApi";

    public string? BaseAddress { get; set; }
    public string? AccessToken { get; set; }
}
