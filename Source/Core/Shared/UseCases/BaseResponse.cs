using Shared.UseCases.Contracts;
using Shared.ValueObjects;

namespace Shared.UseCases;

public class BaseResponse<TData> : IReponseData
{

    public int HttpStatusCode { get; set; }

    public bool IsSuccess => HttpStatusCode >= 200 && HttpStatusCode < 300;

    public string? Message { get; set; } = null;

    private readonly IList<Error> _errors = [];

    public IReadOnlyCollection<Error>? Errors
        => _errors?.ToArray();

    public TData? Data { get; set; }

    public BaseResponse() { }

    public BaseResponse(bool isSuccess, TData data)
    {
        HttpStatusCode = isSuccess ? 200 : 400;
        Data = data;
    }

    public BaseResponse(
        TData? data,
        int httpStatusCode = 200,
        string? message = null)
    {
        Data = data;
        HttpStatusCode = httpStatusCode;
        Message = message;
    }

    public BaseResponse(Error error, int statusCode = 400)
    {
        _errors = [error];
        HttpStatusCode = statusCode;
    }

    public BaseResponse(Error[] errors, int statusCode = 400, string? message = null)
    {
        _errors = errors;
        HttpStatusCode = statusCode;
        Message = message;
    }

    public BaseResponse(Exception exception, int statusCode = 400)
    {
        _errors = [new Error(exception.Message)];
        HttpStatusCode = statusCode;
    }

}
