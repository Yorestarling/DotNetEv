using System.Net;

namespace Common.Responses;


public interface IResponse : IDisposable
{
    bool IsSuccess { get; set; }
    string Message { get; set; }

}


public class ResponseDto<T> : IResponse
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public HttpStatusCode? StatusCode { get; set; }

    public void Dispose() { }

    public static ResponseDto<T> Success<T>(T data, string message)
    {
        return new ResponseDto<T> { Data = data, Message = message };
    }
}

