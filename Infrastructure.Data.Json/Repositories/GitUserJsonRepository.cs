using Gitar.Application.Configuration;
using Gitar.Domain.Contracts.Data;
using Gitar.Domain.Exceptions;
using Gitar.Domain.Models;
using Gitar.Domain.Constants;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Gitar.Domain.Common;
using AutoMapper;

namespace Infrastructure.Data.Json.Repositories;

public class GitUserJsonRepository : IRepository<GitUser, Guid>
{
    private readonly IMapper _mapper;
    private string FullJsonFileName { get; set; }
    private bool UseMinifiedJson { get; set; }
    private List<GitUser> QueuedUsersForInsert { get; set; }

    public GitUserJsonRepository(IMapper mapper, IOptions<DataSourceConfiguration> dataSourceOptions)
    {
        var config = dataSourceOptions?.Value;

        if (config is null)
            throw new InvalidDataSourceException();

        var basePath = string.IsNullOrEmpty(config.AbsolutePath) ? 
            Path.Combine(Environment.CurrentDirectory, "wwwroot") : 
            config.AbsolutePath;

        this.FullJsonFileName = Path.Combine(basePath, $"{config.FileName}{DataSourceFileExtensions.JSON}");
        this.UseMinifiedJson = config.MinifyJson;
        this.QueuedUsersForInsert = new List<GitUser>();
        this._mapper = mapper;

        if (config.ClearOnStartup && File.Exists(this.FullJsonFileName))
        {
            File.Delete(this.FullJsonFileName);
        }

        if (!File.Exists(this.FullJsonFileName))
        {
            using var stream = File.Create(this.FullJsonFileName);
        }
    }

    public async Task<Response<GitUser>> CreateAsync(GitUser entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        this.QueuedUsersForInsert.Add(entity);

        try
        {
            var contentSource = await this.GetDataContent();

            if (contentSource?.FirstOrDefault(e => e.Name == entity.Name) is not null)
            {
                this.QueuedUsersForInsert.Remove(entity);
                throw new EntityAlreadyExistsException();
            }

            foreach (var queuedUser in this.QueuedUsersForInsert)
            {
                contentSource?.Add(queuedUser);
            }

            await this.UpdateDataContent(contentSource);

            // Data inserted successfully, clear the queue
            if (this.QueuedUsersForInsert is not null && this.QueuedUsersForInsert.Any())
            {
                this.QueuedUsersForInsert.Clear();
            }
        }
        catch (IOException)
        {
            return Response<GitUser>.CreateWarning("Insert failed but user added in queue");
        }
        catch (Exception ex)
        {
            return Response<GitUser>.CreateError(ex.Message);
        }

        return Response<GitUser>.CreateSuccess();
    }

    public async Task DeleteAsync(Guid key)
    {
        if (key == Guid.Empty)
            throw new ArgumentException("Invalid key for deletion");

        var content = await this.GetDataContent();

        if (content is not null)
        {
            var entity = content.SingleOrDefault(x => x.Id == key);
            if (entity is not null)
            {
                entity.IsActive = false;

                await this.UpdateDataContent(content);
            }
        }
    }

    public async Task<GitUser?> GetByKeyAsync(Guid key)
    {
        var users = await this.GetAsync(x => x.Id == key);

        return users.SingleOrDefault();
    }

    public async Task<IList<GitUser>> GetAsync(Func<GitUser, bool>? predicate = null)
    {
        var content = await this.GetDataContent();

        if (content is not null)
        {
            var entities = content.AsEnumerable();

            if (predicate is not null) 
                entities = entities.Where(predicate);

            if (entities is not null)
            {
                return entities.Where(x => x.IsActive).ToList();
            }
        }

        return Enumerable.Empty<GitUser>().ToList();
    }

    public async Task UpdateAsync(GitUser entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));

        if (entity.Id == Guid.Empty)
            throw new ArgumentException("Entity identificator is not valid");

        var content = await this.GetDataContent();

        if (content is not null)
        {
            var user = content.SingleOrDefault(x => x.Id == entity.Id);

            if (user is not null)
            {
                var result = _mapper.Map(entity, user);

                await this.UpdateDataContent(content);
            }
        }
    }

    #region Private helper methods

    private async Task<List<GitUser>?> GetDataContent(bool initializeIfNull = true)
    {
        string existingContent = string.Empty;

        using (var reader = new StreamReader(this.FullJsonFileName))
        {
            existingContent = await reader.ReadToEndAsync();
        }

        var contentSource = JsonConvert.DeserializeObject<List<GitUser>>(existingContent);

        if (contentSource is null && initializeIfNull)
            contentSource = new List<GitUser>();

        return contentSource;
    }
    private async Task UpdateDataContent(List<GitUser>? dataContent)
    {
        if (dataContent is not null)
        {
            var contentToRewrite = JsonConvert.SerializeObject(dataContent, this.UseMinifiedJson ? Formatting.None : Formatting.Indented);

            using (var writer = new StreamWriter(this.FullJsonFileName, append: false))
            {
                await writer.WriteAsync(contentToRewrite);
            }
        }
    }

    #endregion
}
