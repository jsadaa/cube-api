using System.ComponentModel.DataAnnotations;

namespace ApiCube.Application.DTOs.Requests.Auth;

public class LoginRequest
{
    [Required] public required string Email { get; set; }

    [Required] public required string Password { get; set; }
}