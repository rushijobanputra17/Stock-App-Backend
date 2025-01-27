using Core.Data;
using Core.Migrations;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.Enums;

namespace Core.Repository
{
    public class PortfolioRepository
    {
        private readonly ApplicationDbContext context;
        public PortfolioRepository(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<JsonResponse> GetAll(string userId)
        {
            JsonResponse res = new JsonResponse();

            try
            {
                var userPortfolio = await context.UserPortfolio.Where(x => x.UserId == userId)
                    .Join(context.Users,
                     port => port.UserId,
                    user => user.Id,
                     (port, user) => new
                     {
                         port.PortfolioId,
                         user.FirstName,
                         user.LastName,
                         port.Quantity,
                         port.PurchasedPrice,
                         port.CreatedDate,
                         port.UpdatedDate,
                         port.StockId,
                         port.UserId
                     }
                    )
                    .Join(context.Stocks, userPorfoion => userPorfoion.StockId
                    , stock => stock.StockId,
                    (userPorfoion, stock) => new
                    {
                        userPorfoion.PortfolioId,
                        userPorfoion.FirstName,
                        userPorfoion.LastName,
                        userPorfoion.Quantity,
                        userPorfoion.PurchasedPrice,
                        userPorfoion.CreatedDate,
                        userPorfoion.UpdatedDate,
                        userPorfoion.StockId,
                        userPorfoion.UserId,
                        stock.StockPrice,
                        stock.CompanyId

                    }
                    )
                    .Join(context.Companys, stockPort => stockPort.CompanyId, comp => comp.CompanyId
                    , (stockPort, comp) => new
                    {
                        stockPort.PortfolioId,
                        stockPort.FirstName,
                        stockPort.LastName,
                        stockPort.Quantity,
                        stockPort.PurchasedPrice,
                        stockPort.CreatedDate,
                        stockPort.UpdatedDate,
                        stockPort.StockId,
                        stockPort.UserId,
                        stockPort.StockPrice,
                        stockPort.CompanyId,
                        comp.CompanyName
                    }
                    )

                    .ToListAsync();

                if (userPortfolio != null)
                {
                    res.Data = userPortfolio;

                    res.Status = (int)APISTATUS.OK;
                }
                else
                {
                    res.Status = (int)APISTATUS.NotFound;
                }
            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.CommonMethods.GetException(ex);
            }


            return res;
        }

        public async Task<JsonResponse> DeleltePortfolio(int portfolioId)
        {
            JsonResponse res = new JsonResponse();
            try
            {
                var commentObj = await context.UserPortfolio.FirstOrDefaultAsync(s => s.PortfolioId == portfolioId);
                if (commentObj != null)
                {
                    context.Remove(commentObj);
                    await context.SaveChangesAsync();
                    res.Status = (int)APISTATUS.OK;

                }
                else
                {

                    res.Status = (int)APISTATUS.NotFound;
                    res.StatusMessage = "please provide valid data";
                }

            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.CommonMethods.GetException(ex);
            }

            return res;
        }

        public async Task<JsonResponse> AddPortfolio(Portfolio model, UserInformation user)
        {

            JsonResponse res = new JsonResponse();
            user = user ?? new UserInformation();
            try
            {
                var userId = user.UserId;
                var stockExist = await context.UserPortfolio.FirstOrDefaultAsync(x => x.UserId == userId && x.StockId == model.StockId);
                if (stockExist != null)
                {
                    res.Status = (int)APISTATUS.OK;
                    res.StatusMessage = "Stock aleady added in portfolio";
                }
                else
                {
                    var stockObj = await context.Stocks.FindAsync(model.StockId);
                    model.UserId = userId;
                    model.CreatedDate = DateTime.Now;
                    model.UpdatedDate = DateTime.Now;
                    model.PurchasedPrice = stockObj != null ? stockObj.StockPrice : 0;

                    context.UserPortfolio.Add(model);
                    await context.SaveChangesAsync();
                    res.Status = (int)APISTATUS.OK;

                }




            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.CommonMethods.GetException(ex);
            }

            return res;
        }

        //public async Task<JsonResponse> GetportfolioById(int postfo)
        //{
        //    JsonResponse res = new JsonResponse();
        //    try
        //    {
        //        var commentObj = await context.UserPortfolio.FirstOrDefaultAsync(s => s.PortfolioId == portfolioId);
        //        if (commentObj != null)
        //        {
        //            context.Remove(commentObj);
        //            await context.SaveChangesAsync();
        //            res.Status = (int)APISTATUS.OK;

        //        }
        //        else
        //        {

        //            res.Status = (int)APISTATUS.NotFound;
        //            res.StatusMessage = "please provide valid data";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        res.Status = (int)APISTATUS.InternalServerError;
        //        res.StatusMessage = CommonMethods.CommonMethods.GetException(ex);
        //    }

        //    return res;
        //}

    }
    }
