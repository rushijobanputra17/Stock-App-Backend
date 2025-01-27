using Core.CommonMethods;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Core.Enums.Enums;

namespace StockMarketApp.Controllers
{
    [Route("api/Company")]
    [ApiController]
    public class CompanyController : Controller
    {
        public readonly IComapnyRepository companyRepository;
        private readonly ITokenService _tokenService;

        public CompanyController(IComapnyRepository DbComapnyRepository,ITokenService tokenService)
        {
            companyRepository = DbComapnyRepository;
            _tokenService = tokenService;
        }


        [HttpGet]
        [Authorize(Policy = "GroupAdminPolicy")]
        public async Task<IActionResult> GetAllCompainines()
        {
            JsonResponse res = new JsonResponse();
            try
            {

                res = await companyRepository.GetAllCompainines();

            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.GetException(ex);

            }

            return Ok(res);
        }


        [HttpGet("{id}")]
        [Authorize(Policy = "GroupAdminPolicy")]
        public async Task<IActionResult> GetCompanyById([FromRoute] int id)
        {
            JsonResponse res = new JsonResponse();
            try
            {
                res = await companyRepository.GetCompanyById(id);
            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.GetException(ex);
                //return BadRequest(res);
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


        [HttpPost("InsertUpdateCompainies")]
        [Authorize(Policy = "GroupAdminPolicy")]
        public async Task<IActionResult> InsertUpdateCompainies([FromBody] AddCompanyModel company)
        {

            JsonResponse res = new JsonResponse();

            try
            {
                if (company != null)
                {
                    res = await companyRepository.InsertUpdateCompainies(company, GetLongUserInfo());
                }
                else
                {
                    res.Status = (int)APISTATUS.BadRequest;
                    res.StatusMessage = "please provide valid data";
                }
            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.GetException(ex);
            }

            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "GroupAdminPolicy")]
        public async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            JsonResponse res = new JsonResponse();
            try
            {
                res = await companyRepository.DeleteCompanyById(id);
            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.GetException(ex);
                //return BadRequest(res);
            }

            return Ok(res);
        }
    }
}
