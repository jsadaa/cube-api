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
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;
    private readonly UserManager<ApplicationUserModel> _userManager;

    public AuthService(UserManager<ApplicationUserModel> userManager, IConfiguration configuration,
        ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<BaseResponse> Login(LoginRequest loginRequest)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                throw new IdentifiantsIncorrects();

            var tokenString = await GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            const string refreshTokenName = "RefreshToken";
            await _userManager.SetAuthenticationTokenAsync(user, "ApiCube", refreshTokenName, refreshToken);

            var tokenResponse = new TokenResponse
            {
                AccessToken = tokenString,
                RefreshToken = refreshToken
            };

            var response = new BaseResponse(
                HttpStatusCode.OK,
                tokenResponse
            );

            return response;
        }
        catch (IdentifiantsIncorrects e)
        {
            var response = new BaseResponse(
                HttpStatusCode.Unauthorized,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
            );

            return response;
        }
    }

    public async Task<BaseResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest)
    {
        try
        {
            var principal = GetPrincipalFromExpiredToken(refreshTokenRequest.ExpiredToken);
            var username = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username)) throw new ClaimInvalide();

            var user = await _userManager.FindByNameAsync(username);
            if (user == null) throw new UtilisateurIntrouvable();

            var storedRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, "ApiCube", "RefreshToken");
            if (storedRefreshToken != refreshTokenRequest.RefreshToken) throw new RefreshTokenInvalide();

            var newJwtToken = await GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            await _userManager.SetAuthenticationTokenAsync(user, "ApiCube", "RefreshToken", newRefreshToken);

            var tokenResponse = new TokenResponse
            {
                AccessToken = newJwtToken,
                RefreshToken = newRefreshToken
            };

            var response = new BaseResponse(
                HttpStatusCode.OK,
                tokenResponse
            );

            return response;
        }
        catch (UtilisateurIntrouvable e)
        {
            var response = new BaseResponse(
                HttpStatusCode.NotFound,
                new { code = e.Message }
            );

            return response;
        }
        catch (ClaimInvalide e)
        {
            var response = new BaseResponse(
                HttpStatusCode.Unauthorized,
                new { code = e.Message }
            );

            return response;
        }
        catch (RefreshTokenInvalide e)
        {
            var response = new BaseResponse(
                HttpStatusCode.Unauthorized,
                new { code = e.Message }
            );

            return response;
        }
        catch (SecurityTokenException e)
        {
            var response = new BaseResponse(
                HttpStatusCode.Unauthorized,
                new { code = e.Message }
            );

            return response;
        }
        catch (Exception e)
        {
            var response = new BaseResponse(
                HttpStatusCode.InternalServerError,
                new { code = "unexpected_error", message = e.Message }
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
            new(JwtRegisteredClaimNames.Sub,
                user.UserName ?? throw new InvalidOperationException("User.UserName is null")),
            new(JwtRegisteredClaimNames.Email, user.Email ?? throw new InvalidOperationException("User.Email is null")),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        claims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
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
        var tokenHandler = new JwtSecurityTokenHandler();

        if (tokenHandler.ReadToken(token) is not JwtSecurityToken securityToken ||
            !securityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("jwt_token_invalide");

        if (securityToken.ValidTo > DateTime.UtcNow)
            throw new SecurityTokenException("token_non_expire");

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

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
        if (validatedToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}