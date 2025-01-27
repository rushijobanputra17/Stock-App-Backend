using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public  interface ITokenService
    {
        public string CreateToken(AppUser appUser);

        public UserInformation GetLoginUserInformation(ClaimsPrincipal User);

        string CreateRefreshToken(AppUser appUser);

        ClaimsPrincipal GetRefreshTokenPrincipal(string refreshToken);


    }
}
