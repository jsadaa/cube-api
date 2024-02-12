namespace ApiCube.Application.DTOs.Responses;

public class TokenResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public required int ClientId { get; set; }
}