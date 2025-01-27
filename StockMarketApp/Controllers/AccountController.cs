using Azure;
using Core.CommonMethods;
using Core.Data;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;
using System.IdentityModel.Tokens.Jwt;
using static Core.Enums.Enums;

namespace StockMarketApp.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public readonly UserManager<AppUser> _UserManager;

        private readonly ITokenService _tokenService;

        public readonly ApplicationDbContext context;

        private readonly SignInManager<AppUser> signInManager;


        public AccountController(ApplicationDbContext dbContext,UserManager<AppUser> userManager, ITokenService tokenService,SignInManager<AppUser> signIn) {
            _UserManager = userManager;
            _tokenService = tokenService;
            signInManager = signIn;
            context = dbContext;

        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register([FromBody] AddUserAccount userObj)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                response = await RegisterUser(userObj, "user");
            }
            catch (Exception ex)
            {
                response.Status = (int)APISTATUS.InternalServerError;
                response.StatusMessage = CommonMethods.GetException(ex);
            }
            return Ok(response);
        }

        [HttpPost("RegisterCompanyAdmin")]
        public async Task<IActionResult> RegisterCompanyUser([FromBody] AddUserAccount userObj)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                response = await RegisterUser(userObj, "CompanyAdmin");
            }
            catch (Exception ex)
            {
                response.Status = (int)APISTATUS.InternalServerError;
                response.StatusMessage = CommonMethods.GetException(ex);
            }
            return Ok(response);
        }

        private UserInformation GetUserTokenInfo(AppUser appUser,bool isCreateRefreshToken=true)
        {
           var  userObj = new UserInformation();    
            try
            {
                 userObj = new UserInformation()
                {
                    Email = appUser.Email,
                    UserName = appUser.UserName,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName

                };

                var token = _tokenService.CreateToken(appUser);
                userObj.Token = token;
                if (isCreateRefreshToken)
                {
                    var refreshToken = _tokenService.CreateRefreshToken(appUser);

                    appUser.RefreshToken = refreshToken;
                    context.Users.Update(appUser);
                    context.SaveChanges();

                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddDays(7)
                    };
                    Response.Cookies.Append("UserRefreshToken", refreshToken, cookieOptions);
                }
                



            }
            catch (Exception ex)
            {
                return null;
            }
            return userObj;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _UserManager.Users.FirstOrDefaultAsync(x=> x.UserName.ToLower()==model.UserName.ToLower() || x.Email.ToLower()==model.UserName.ToLower());
                if (user==null)
                {
                    response.Status = (int)APISTATUS.Unauthorized;
                    response.StatusMessage ="No User found.";
                }
                else
                {
                    var result = await signInManager.CheckPasswordSignInAsync(user,model.Password,false);
                    if (result!=null && result.Succeeded)
                    {
                        response.Status = (int)APISTATUS.OK;
                     
                        response.Data = GetUserTokenInfo(user);

                    }
                    else
                    {
                        response.Status = (int)APISTATUS.Unauthorized;
                        response.StatusMessage = "Invalid unser cred.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = (int)APISTATUS.InternalServerError;
                response.StatusMessage = CommonMethods.GetException(ex);
            }
            return Ok(response);
        }

        private async Task<JsonResponse> RegisterUser(AddUserAccount userObj, string roleName)
        {
            JsonResponse response = new JsonResponse();
            try
            {
               
                var appUser = new AppUser
                {

                    UserName = userObj.UserName,
                    Email = userObj.Email,
                    FirstName = userObj.FirstName,
                    LastName = userObj.LastName
                };

                var createUser = await _UserManager.CreateAsync(appUser, userObj.Password);

                if (createUser.Succeeded)
                {
                    var roleResult = await _UserManager.AddToRoleAsync(appUser, roleName);
                    if (roleResult.Succeeded)
                    {
                        response.Status = (int)APISTATUS.OK;
                      
                        response.Data = GetUserTokenInfo(appUser);

                    }
                    else
                    {
                        response.Status = (int)APISTATUS.InternalServerError;
                        response.Data = roleResult.Errors;
                    }
                }
                else
                {
                    response.Status = (int)APISTATUS.InternalServerError;
                    response.Data = createUser.Errors;
                }
            }
            catch (Exception ex)
            {
                response.Status = (int)APISTATUS.InternalServerError;
                response.StatusMessage = CommonMethods.GetException(ex);
            }
            return response;
        }

        [HttpPost("RefreshToken")]
        public  async Task<IActionResult> RefreshToken()
        {
            JsonResponse response = new JsonResponse();
            try
            {
                var refreshToken = Request.Cookies["UserRefreshToken"];
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    var principal = _tokenService.GetRefreshTokenPrincipal(refreshToken);
                    if (principal != null)
                    {
                        var usernameClaim = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name);
                       
                        var userName = usernameClaim?.Value;

                        var appUser = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
                        if (appUser != null && !string.IsNullOrEmpty(appUser.RefreshToken) && refreshToken == appUser.RefreshToken)
                        {

                            var exp = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value;
                            if (exp != null && long.TryParse(exp, out var expSeconds))
                            {
                            
                                if (DateTimeOffset.FromUnixTimeSeconds(expSeconds).UtcDateTime > DateTime.Now)
                                {
                                    response.Status = (int)APISTATUS.OK;
                                    response.Data = GetUserTokenInfo(appUser, false);
                                    return Ok(response);
                                }
                              
                            }

                           
                        }


                    }
                }
                response.Status = (int)APISTATUS.Unauthorized;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = (int)APISTATUS.InternalServerError;
                response.StatusMessage = CommonMethods.GetException(ex);
            }

            return Ok(response);
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            JsonResponse response = new JsonResponse();
            try
            {
                var refreshToken = Request.Cookies["UserRefreshToken"];
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    // Remove the refresh token from the database
                    var principal = _tokenService.GetRefreshTokenPrincipal(refreshToken);
                    var usernameClaim = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);

                    var userName = usernameClaim?.Value;

                    var appUser = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
                    if (appUser != null)
                    {
                        appUser.RefreshToken = null;
                        context.Update(appUser);
                        context.SaveChanges();
                    }
                }

                // Clear the cookie
                Response.Cookies.Append("UserRefreshToken", "", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(-1) // Expire immediately
                });

                response.Status = (int)APISTATUS.OK;
            }
            catch (Exception ex)
            {
                response.Status = (int)APISTATUS.InternalServerError;
                response.StatusMessage = CommonMethods.GetException(ex);
            }
            


            return Ok(response);
        }
    }
}
