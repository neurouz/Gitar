namespace Gitar.Application.Configuration;

public class DataSourceConfiguration
{
    public static string CONFIGNAME = "DataSource";

    public string? AbsolutePath { get; set; }
    public string? FileName { get; set; }
    public bool MinifyJson { get; set; }
    public bool ClearOnStartup { get; set; }
}
