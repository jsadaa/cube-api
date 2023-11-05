using System.Net;

namespace ApiCube.Application.DTOs;

public class BaseResponse
{
    public int StatusCode { get; set; }
    public object Data { get; set; }
    
    public BaseResponse(HttpStatusCode statusCode, object data)
    {
        StatusCode = (int) statusCode;
        Data = data;
    }
    
    /// <summary>
    /// Set the response status code and data
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="data"></param>
    public void SetResponse(HttpStatusCode statusCode, object data)
    {
        StatusCode = (int) statusCode;
        Data = data;
    }
}