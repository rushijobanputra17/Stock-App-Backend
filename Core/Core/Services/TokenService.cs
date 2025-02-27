using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
        }

        public UserInformation GetLoginUserInformation(ClaimsPrincipal User)
        {
            UserInformation userInformation = new UserInformation();
            var userName = User.FindFirst(ClaimTypes.GivenName)?.Value;
            if (userName == null)
            {
                return null;
            }
            else
            {
                var appUser = _userManager.Users.FirstOrDefault(x => x.UserName.ToLower() == userName);
                if (appUser != null)
                {
                    userInformation = new UserInformation()
                    {
                        FirstName = appUser.FirstName,
                        LastName = appUser.LastName,
                        Email = appUser.Email,
                        UserId = appUser.Id,
                        UserName = appUser.UserName
                    };
                }
                return userInformation; ;
            }

            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // User ID (if stored in token)
            //var username = User.Identity.Name; // Username (if provided during login)
            //var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList(); // List of roles
            //var email = User.FindFirst(ClaimTypes.GivenName)?.Value; // User email (if provided during login)
            return null;
        }

        public async Task<String> CreateToken(AppUser appUser)
        {

            var userRoles = await _userManager.GetRolesAsync(appUser);


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,appUser.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,appUser.UserName),
                 new Claim("UserId", appUser.Id.ToString()),
               new Claim("Role", string.Join(",", userRoles))


            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1)
                ,
                SigningCredentials = creds
                ,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);

        }

        public string CreateRefreshToken(AppUser appUser)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, appUser.Id.ToString()),
         new Claim(JwtRegisteredClaimNames.Name, appUser.UserName),
         new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7), // Refresh token valid for 7 days
                SigningCredentials = creds,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal GetRefreshTokenPrincipal(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true, // Ensures expiration is checked
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["JWT:Issuer"],
                    ValidAudience = _configuration["JWT:Audience"],
                    IssuerSigningKey = _key
                }, out SecurityToken validatedToken);

                return principal; // If valid, return the claims principal
            }
            catch
            {
                // Handle validation errors
                return null;
            }
        }


    }
}
