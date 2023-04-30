using Gitar.Domain.Contracts.Data;

namespace Gitar.Domain.Common;

public class Response<T> where T : IEntityBase
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? ResponseObject { get; set; }

    public static Response<T> CreateSuccess(string? msg = null, T? instance = default(T)) 
        => new Response<T> { Success = true, Message = string.IsNullOrEmpty(msg) ? "Success" : msg, ResponseObject = instance };

    public static Response<T> CreateError(string? msg = null, T? instance = default(T)) 
        => new Response<T> { Success = false, Message = string.IsNullOrEmpty(msg) ? "Error" : msg, ResponseObject = instance };

    public static Response<T> CreateWarning(string? msg = null, T? instance = default(T))
    => new Response<T> { Success = false, Message = string.IsNullOrEmpty(msg) ? "Warning" : msg, ResponseObject = instance };
}
