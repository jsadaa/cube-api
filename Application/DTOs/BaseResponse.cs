using System.Net;

namespace ApiCube.Application.DTOs;

public class BaseResponse
{
    public BaseResponse(HttpStatusCode statusCode, object data)
    {
        StatusCode = (int)statusCode;
        Data = data;
    }

    public int StatusCode { get; set; }
    public object Data { get; set; }

    /// <summary>
    ///     Set the response status code and data
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="data"></param>
    public void SetResponse(HttpStatusCode statusCode, object data)
    {
        StatusCode = (int)statusCode;
        Data = data;
    }
}