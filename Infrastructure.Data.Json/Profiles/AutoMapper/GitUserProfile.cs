using AutoMapper;
using Gitar.Domain.Models;

namespace Infrastructure.Data.Json.Profiles.AutoMapper;

public class GitUserProfile : Profile
{
    public GitUserProfile()
    {
        CreateMap<GitUser, GitUser>().ReverseMap();
    }
}
