using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using ApiCube.Application.DTOs;
using ApiCube.Application.DTOs.Requests.Auth;
using ApiCube.Application.DTOs.Responses;
using ApiCube.Application.Exceptions;
using ApiCube.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ApiCube.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUserModel> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUserModel> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<BaseResponse> Login(LoginRequest loginRequest)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                throw new EmailOuMotDePasseIncorrect();

            var tokenString = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            const string refreshTokenName = "RefreshToken";
            await _userManager.SetAuthenticationTokenAsync(user, "Cube", refreshTokenName, refreshToken);

            var tokenResponse = new TokenResponse()
            {
                AccessToken = await tokenString,
                RefreshToken = refreshToken
            };

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: tokenResponse
            );

            return response;
        }
        catch (EmailOuMotDePasseIncorrect e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Unauthorized,
                data: new { message = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );

            return response;
        }
    }

    public async Task<BaseResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest)
    {
        try
        {
            var principal = GetPrincipalFromExpiredToken(refreshTokenRequest.ExpiredToken);
            var username = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) throw new UtilisateurIntrouvable();

            var storedRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, "MyApp", "RefreshToken");

            if (storedRefreshToken != refreshTokenRequest.RefreshToken) throw new RefreshTokenInvalide();

            var newJwtToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            await _userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", newRefreshToken);

            var tokenResponse = new TokenResponse()
            {
                AccessToken = await newJwtToken,
                RefreshToken = newRefreshToken
            };

            var response = new BaseResponse(
                statusCode: HttpStatusCode.OK,
                data: tokenResponse
            );

            return response;
        }
        catch (UtilisateurIntrouvable e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Unauthorized,
                data: new { message = e.Message }
            );

            return response;
        }
        catch (RefreshTokenInvalide e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.Unauthorized,
                data: new { message = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                statusCode: HttpStatusCode.InternalServerError,
                data: new { message = e.Message }
            );

            return response;
        }
    }

    private async Task<string> GenerateJwtToken(ApplicationUserModel user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ??
                                                                          throw new InvalidOperationException(
                                                                              "Jwt:Key is null")));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userRoles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserName),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        claims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ??
                                                                               throw new InvalidOperationException(
                                                                                   "Jwt:Key is null"))),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}