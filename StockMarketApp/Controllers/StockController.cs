using Azure.Core;
using Core.CommonMethods;
using Core.Data;
using Core.Helpers;
using Core.Interfaces;
using Core.Migrations;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Claims;
using System.Xml.Linq;
using static Core.Enums.Enums;

namespace StockMarketApp.Controllers
{
    [Route("api/Stock")]
    [ApiController]
    public class StockController : Controller
    {
        //public readonly ApplicationDbContext context;

        public readonly IStockRepository _stockRepo;
        private readonly ITokenService _tokenService;




        public StockController(ApplicationDbContext dbContext,IStockRepository stockRepository, ITokenService tokenService)
        {
            _stockRepo = stockRepository;
            _tokenService = tokenService;
            //context = dbContext;
        }


        

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllStocks([FromQuery] QueryParameters query)
        {
            JsonResponse res = new JsonResponse();
            try
            {
               
                //_tokenService.GetLoginUserInformation(User);
                var user = GetLongUserInfo();
                res = await _stockRepo.GetAllStocks(query, user);
               
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


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            JsonResponse res = new JsonResponse();
            try
            {
                res = await  _stockRepo.GetStocksById(id);



                //if (stock != null)
                //{
                //    res.Data = stock;
                //    res.Status = (int)APISTATUS.OK;
                //}
                //else
                //{
                //    res.Status = (int)APISTATUS.NotFound;
                //}
            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.GetException(ex);
                //return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost("AddEditStockDetails")]
        [Authorize(Policy = "GroupAdminPolicy")]
        public async Task<IActionResult> AddEditStockDetails([FromBody] StockCreateModel stock)
        {

            JsonResponse res = new JsonResponse();

            try
            {
                if (stock != null)
                {
                    res  =  await _stockRepo.AddEditStockDetails(stock, GetLongUserInfo());
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
        public  async Task<IActionResult> DeleteById([FromRoute] int id)
        {
            JsonResponse res = new JsonResponse();
            try
            {
                res = await _stockRepo.DeleteById(id);
            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.GetException(ex);
                //return BadRequest(res);
            }

            return Ok(res);
        }


        [HttpGet("GetAllComments")]
        

        public async Task<IActionResult> GetAllComments()
        {
            JsonResponse res = new JsonResponse();
            try
            {
                res.Data = await _stockRepo.GetAllStockComments();
                res.Status = (int)APISTATUS.OK;
            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.GetException(ex);
            }
            return Ok(res);
        }


        [HttpGet("GetCommentsByStockId")]
        [Authorize]
        public async Task<IActionResult> GetCommentsByStockId([FromRoute] int stockId)
        {

            JsonResponse res = new JsonResponse();
            try
            {
                res.Data = await _stockRepo.GetCommentsByStockId(stockId);
                res.Status = (int)APISTATUS.OK;
            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.GetException(ex);
            }
            return Ok(res);
        }

        [HttpPost("InsertUpdateStockComment")]
        [Authorize]
        public async Task<IActionResult> InsertUpdateStockComment([FromRoute] CommentSaveModel model)
        {
            JsonResponse res = new JsonResponse();
            try
            {
                if (model!=null)
                {
                    res.Data = await _stockRepo.CreateUpdateComments(model);
                    res.Status = (int)APISTATUS.OK;
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

        [HttpDelete]
        [Route("DeleteStockComments")]
        [Authorize]

        public async Task<IActionResult> DeleteStockComment([FromRoute] int stockId,int commentId)
        {
            var res = new JsonResponse();
            try
            {
                if (stockId > 0 && commentId > 0)
                {
                    res =  await _stockRepo.DeleteStockComment(stockId,commentId);
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
        
    }
}
