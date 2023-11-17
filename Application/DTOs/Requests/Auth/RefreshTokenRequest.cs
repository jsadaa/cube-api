using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests.Auth;

public class RefreshTokenRequest
{
    [Required] public required string ExpiredToken { get; set; }

    [Required] public required string RefreshToken { get; set; }
}