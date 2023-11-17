using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests.Auth;

namespace ApiCube.Application.Services.Auth;

public interface IAuthService
{
    public Task<BaseResponse> Login(LoginRequest request);
    public Task<BaseResponse> RefreshToken(RefreshTokenRequest request);
}