using Gitar.Application.Configuration;
using Gitar.Domain.Contracts.Data;
using Gitar.Domain.Exceptions;
using Gitar.Domain.Models;
using Gitar.Domain.Constants;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.IO;
using System.Text;

namespace Infrastructure.Data.Json.Repositories;

public class GitUserJsonRepository : IRepository<GitUser, int>
{
    private string? FullJsonFileName { get; set; }
    public GitUserJsonRepository(IOptions<DataSourceConfiguration> dataSourceOptions)
    {
        var config = dataSourceOptions?.Value;

        if (config is null)
            throw new InvalidDataSourceException();

        var basePath = string.IsNullOrEmpty(config.AbsolutePath) ? 
            Path.Combine(Environment.CurrentDirectory, "wwwroot") : 
            config.AbsolutePath;

        this.FullJsonFileName = Path.Combine(basePath, $"{config.FileName}{DataSourceFileExtensions.JSON}");
    }

    public Task<GitUser> CreateAsync(GitUser entity)
    {
        Console.WriteLine($"Creating entity.. path={this.FullJsonFileName}");

        if (File.Exists(this.FullJsonFileName))
        {
            File.Delete(this.FullJsonFileName);
        }

        using (FileStream fs = File.Create(this.FullJsonFileName))
        {
            AddText(fs, "This is some text");
            AddText(fs, "This is some more text,");
            AddText(fs, "\r\nand this is on a new line");
            AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");
        }

        return null;
    }

    private static void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }

    public Task<GitUser> DeleteAsync(int key)
    {
        throw new NotImplementedException();
    }

    public Task<GitUser> GetAsync(Expression<Func<GitUser, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<GitUser> UpdateAsync(GitUser entity)
    {
        throw new NotImplementedException();
    }
}
