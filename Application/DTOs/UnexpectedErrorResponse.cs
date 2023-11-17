namespace ApiCube.Application.DTOs;

public class UnexpectedErrorResponse
{
    public required string Code { get; set; }
    public required string Message { get; set; }
}