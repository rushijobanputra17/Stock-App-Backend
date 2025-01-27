using Core.CommonMethods;
using Core.Data;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Core.Enums.Enums;

namespace StockMarketApp.Controllers
{
    [Route("api/Portfolio")]
    [ApiController]
    public class PortfolioController : Controller
    {
        public readonly IPortfolioRepository _portfolioRepo;
        private readonly ITokenService _tokenService;

        public PortfolioController(ApplicationDbContext dbContext, IPortfolioRepository portfolioRepo, ITokenService tokenService)
        {
            _portfolioRepo = portfolioRepo;
            _tokenService = tokenService;
            //context = dbContext;
        }


        [HttpGet("getallportfolio")]
        [Authorize]
        public async Task<IActionResult> GetallportfolioUserWise()
        {
            JsonResponse res = new JsonResponse();
            try
            {

                //_tokenService.GetLoginUserInformation(User);
                res = await _portfolioRepo.GetAll(GetLongUserInfo()!=null ? GetLongUserInfo().UserId:"");

            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.GetException(ex);

            }

            return Ok(res);
        }

        [HttpDelete("DeleltePortfolio")]
        [Authorize]
        public async Task<IActionResult> DeleltePortfolio([FromRoute] int portfolioId)
        {
            JsonResponse res = new JsonResponse();
            try
            {

                //_tokenService.GetLoginUserInformation(User);
                res = await _portfolioRepo.DeleltePortfolio(portfolioId);

            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.GetException(ex);

            }

            return Ok(res);
        }

        private UserInformation GetLongUserInfo()
        {
            UserInformation userInformation = new UserInformation();
            try
            {
                userInformation = _tokenService.GetLoginUserInformation(User);
            }
            catch (Exception ex)
            {
                userInformation = null;
            }
            return userInformation;

        }

        [HttpPost("AddPortfolio")]
        [Authorize]
        public async Task<IActionResult> AddPortfolio([FromBody] Portfolio stock)
        {
            JsonResponse res = new JsonResponse();
            try
            {

               
                res = await _portfolioRepo.AddPortfolio(stock, GetLongUserInfo());

            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.GetException(ex);

            }

            return Ok(res);
        }



    }
}
