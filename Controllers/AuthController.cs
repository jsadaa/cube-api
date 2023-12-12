using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests.Auth;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ApiCube.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    ///     Login d'un employé ou d'un client
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="401">identifiants_incorrects</response>
    /// <response code="500">unexpected_error</response>
    [HttpPost("login")]
    [ActionName("Login")]
    [ProducesResponseType(typeof(TokenResponse), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 401)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    [Produces("application/json")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var response = await _authService.Login(loginRequest);
        return StatusCode(response.StatusCode, response.Data);
    }


    /// <summary>
    ///     Rafraîchir un token expiré
    /// </summary>
    /// <param name="refreshTokenRequest"></param>
    /// <returns></returns>
    /// <response code="200"></response>
    /// <response code="401">claim_invalide | refresh_token_invalide | jwt_token_invalide | token_non_expire</response>
    /// <response code="500">unexpected_error</response>
    [HttpPost("refresh-token")]
    [ActionName("RefreshToken")]
    [ProducesResponseType(typeof(TokenResponse), 200)]
    [ProducesResponseType(typeof(ExpectedErrorResponse), 401)]
    [ProducesResponseType(typeof(UnexpectedErrorResponse), 500)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
    {
        var response = await _authService.RefreshToken(refreshTokenRequest);
        return StatusCode(response.StatusCode, response.Data);
    }
}