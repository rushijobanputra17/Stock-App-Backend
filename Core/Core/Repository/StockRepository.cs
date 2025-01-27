using Core.Data;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Schema;
using static Core.Enums.Enums;

namespace Core.Repository
{
   
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext context;
        public StockRepository(ApplicationDbContext dbContext) {
            context = dbContext;
        }

        public async Task<JsonResponse> AddEditStockDetails(StockCreateModel stock, UserInformation user)
        {
            user = user ?? new UserInformation();
            JsonResponse res = new JsonResponse();
            if (stock != null)
            {
                Stock stockObj = new Stock();
                if (stock.StockId > 0)
                {
                    stockObj = await GetStockModel(stock.StockId); //await context.Stocks.FirstOrDefaultAsync(s => s.StockId == stock.StockId);
                    if (stockObj != null)
                    {


                        stockObj.StockName = stock.StockName;
                        stockObj.MarketCap = stock.MarketCap;
                        stockObj.StockPrice = stock.StockPrice;
                        stockObj.LastDevidend = stock.LastDevidend;
                        stockObj.StockType = stock.StockType;
                        stockObj.Symbol = stock.Symbol;
                        stockObj.LastUpdatedTime = DateTime.Now;
                        stockObj.CompanyId = stock.CompanyId;

                        stockObj.UpdatedBy = user.UserId;

                        stockObj.UpdatedDate = DateTime.Now;

                        context.Stocks.Update(stockObj);
                        await context.SaveChangesAsync();

                        res.Status
                             = (int)APISTATUS.OK;
                        res.StatusMessage = "records updated successfully";
                        res.Data = stockObj.StockId;
                    }

                    else
                    {
                        res.Status = (int)APISTATUS.BadRequest;
                        res.StatusMessage = "please provide valid data";
                    }

                }
                else
                {
                    stockObj = new Stock()
                    {
                        StockId = 0,
                        StockName = stock.StockName,
                        MarketCap = stock.MarketCap,
                        StockPrice = stock.StockPrice,
                        LastDevidend = stock.LastDevidend,
                        StockType = stock.StockType,
                        Symbol = stock.Symbol,
                        LastUpdatedTime = DateTime.Now,
                        CompanyId = stock.CompanyId,
                        CreatedBy = user.UserId,
                        UpdatedBy = user.UserId,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };

                    context.Stocks.Add(stockObj);
                    await context.SaveChangesAsync();
                    res.Data = stockObj.StockId;
                    res.Status
                            = (int)APISTATUS.OK;
                }
            }
            else
            {
                res.Status = (int)APISTATUS.BadRequest;
                res.StatusMessage = "please provide valid data";
            }

            return res;
            // stockObj = await context.Stocks.FirstOrDefaultAsync(s => s.StockId == );
        }

        public async Task<Stock> GetStockModel(int id)
        {
            return  await context.Stocks.FirstOrDefaultAsync(s => s.StockId == id);
        }

        public  async Task<JsonResponse> GetAllStocks(QueryParameters QueryParameters)
        {

            JsonResponse res = new JsonResponse();
            try
            {
                res.Status = (int)APISTATUS.OK;
                var queryData = context.Stocks
    .Join(context.Companys.AsNoTracking(),
        stock => stock.CompanyId,
        company => company.CompanyId,
        (stock, company) => new
        {
            stock.StockId,
            stock.StockName,
            stock.MarketCap,
            stock.StockPrice,
            stock.LastDevidend,
            stock.StockType,
            stock.Symbol,
            stock.LastUpdatedTime,
            stock.CreatedBy,
            stock.UpdatedBy,
            stock.CreatedDate,
            stock.UpdatedDate,
            stock.CompanyId,
            CompanyName = company.CompanyName
        })
    .AsQueryable();

                if (QueryParameters != null)
                {
                    if (!string.IsNullOrEmpty(QueryParameters.Name))
                    {
                        string queryName = QueryParameters.Name.ToLower();
                        queryData = queryData.Where(x =>
                            (!string.IsNullOrEmpty(x.StockName) && x.StockName.ToLower().Contains(queryName)) ||
                            (!string.IsNullOrEmpty(x.CompanyName) && x.CompanyName.ToLower().Contains(queryName))
                        );
                    }

                    if (!string.IsNullOrEmpty(QueryParameters.Symbol))
                    {
                        string querySymbol = QueryParameters.Symbol.ToLower();
                        queryData = queryData.Where(x =>
                            !string.IsNullOrEmpty(x.Symbol) && x.Symbol.ToLower().Contains(querySymbol)
                        );
                    }

                    if (!string.IsNullOrEmpty(QueryParameters.SortBy))
                    {
                        string sort = QueryParameters.SortBy.ToLower();
                        if (sort=="stockname")
                        {
                            queryData = QueryParameters.IsDecending ? queryData.OrderByDescending(x => x.StockName) : queryData.OrderBy(x => x.StockName);
                        }

                        if (sort == "stockprice")
                        {
                            queryData = QueryParameters.IsDecending ? queryData.OrderByDescending(x => x.StockPrice) : queryData.OrderBy(x => x.StockPrice);
                        }

                        if (sort == "symbol")
                        {
                            queryData = QueryParameters.IsDecending ? queryData.OrderByDescending(x => x.Symbol) : queryData.OrderBy(x => x.Symbol);
                        }
                    }
                    else
                    {
                        queryData = QueryParameters.IsDecending ? queryData.OrderByDescending(x => x.UpdatedDate) : queryData.OrderBy(x => x.UpdatedDate);
                    }
                }

                var skipNumber = (QueryParameters.PageNumber - 1) * QueryParameters.Pagesize;

                var records = await queryData.Skip(skipNumber).Take(QueryParameters.Pagesize).ToListAsync();
                if (records!=null && records.Any())
                {
                    res.TotalRecords = records.Count;
                    res.Data = records;
                }

                //   var queryData=   context.Stocks
                //.Join(context.Companys.AsNoTracking(),
                //      stock => stock.CompanyId,
                //      company => company.CompanyId,
                //      (stock, company) => new
                //      {
                //          stock.StockId,
                //          stock.StockName,
                //          stock.MarketCap,
                //          stock.StockPrice,
                //          stock.LastDevidend,
                //          stock.StockType,
                //          stock.Symbol,
                //          stock.LastUpdatedTime,
                //          stock.CreatedBy,
                //          stock.UpdatedBy,
                //          stock.CreatedDate,
                //          stock.UpdatedDate,
                //          stock.CompanyId,
                //          CompanyName = company.CompanyName
                //      })
                //.AsQueryable();
                //    if (QueryParameters!=null)
                //    {

                //        if (!string.IsNullOrEmpty(QueryParameters.Name))
                //        {
                //            string queryName = QueryParameters.Name.ToLower();
                //            queryData = queryData.Where(x =>
                //                (!string.IsNullOrEmpty(x.StockName) && x.StockName.ToLower() == queryName) ||
                //                (!string.IsNullOrEmpty(x.CompanyName) && x.CompanyName.ToLower() == queryName)
                //            );
                //        }

                //        if (!string.IsNullOrEmpty(QueryParameters.Symbol))
                //        {
                //            string querySymbol = QueryParameters.Symbol.ToLower();
                //            queryData = queryData.Where(x =>
                //                (!string.IsNullOrEmpty(x.StockName) && x.StockName.ToLower() == queryName) ||
                //                (!string.IsNullOrEmpty(x.CompanyName) && x.CompanyName.ToLower() == queryName)
                //            );
                //        }


                //        queryData = queryData.Where(x =>

                //       ((!string.IsNullOrEmpty(QueryParameters.Name) ?
                //         (!string.IsNullOrEmpty(x.StockName) ? x.StockName.ToLower() == QueryParameters.Name.ToLower() : true) ||

                //         (!string.IsNullOrEmpty(x.CompanyName) ? x.CompanyName.ToLower() == QueryParameters.Name.ToLower() : true)
                //        : true) &&
                //         ((!string.IsNullOrEmpty(QueryParameters.Symbol)) ?
                //            (!string.IsNullOrEmpty(x.Symbol) ? x.Symbol.ToLower() == QueryParameters.Symbol.ToLower() : true)

                //         : true)
                //        )

                //        //(!string.IsNullOrEmpty(x.StockName)? x.StockName.ToLower():"") == (QueryParameters.Name.ToLower())) :true)




                //        );
                //    }

                //    res.Data = await queryData.ToListAsync();
            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.CommonMethods.GetException(ex);  
            }

            return res;

           
        }

        public async Task<JsonResponse> GetStocksById(int id)
        {

            JsonResponse res = new JsonResponse();
            try
            {
                res.Status = (int)APISTATUS.OK;
                var stock  = await context.Stocks.Join(context.Companys, stock => stock.CompanyId,
                    company => company.CompanyId,
                     (stock, company) => new
                     {
                         stock.StockId,
                         stock.StockName,
                         stock.MarketCap,
                         stock.StockPrice,
                         stock.LastDevidend,
                         stock.StockType,
                         stock.Symbol,
                         stock.LastUpdatedTime,
                         stock.CreatedBy,
                         stock.UpdatedBy,
                         stock.CreatedDate,
                         stock.UpdatedDate,
                         stock.CompanyId,
                         CompanyName = company.CompanyName
                     })
                    .GroupJoin(context.StockComments,
                     stockWithCompany => stockWithCompany.StockId,
                    comment => comment.StockId,
                    (stockWithCompany, comment) => new
                    {
                        stockWithCompany.StockId,
                        stockWithCompany.StockName,
                        stockWithCompany.MarketCap,
                        stockWithCompany.StockPrice,
                        stockWithCompany.LastDevidend,
                        stockWithCompany.StockType,
                        stockWithCompany.Symbol,
                        stockWithCompany.LastUpdatedTime,
                        stockWithCompany.CreatedBy,
                        stockWithCompany.UpdatedBy,
                        stockWithCompany.CreatedDate,
                        stockWithCompany.UpdatedDate,
                        stockWithCompany.CompanyId,
                        stockWithCompany.CompanyName,
                        Comments = comment.ToList()

                    })
                    .Where(s => s.StockId == id).FirstOrDefaultAsync();

                if (stock != null)
                {
                    res.Data = stock;
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

        public async Task<JsonResponse> DeleteById(int id)
        {
            JsonResponse res = new JsonResponse();
          
            try
            {
                var stock = await GetStockModel(id);

                if (stock != null)
                {
                    res.Data = stock;
                    context.Stocks.Remove(stock);
                    context.SaveChanges();
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

        public async Task<List<Comments>> GetAllStockComments()
        {
            try
            {
                return await context.StockComments.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
           
        }


        public async Task<List<Comments>> GetCommentsByStockId(int stockId)
        {
            

            try
            {
                return await context.StockComments.Where(x => x.StockId == stockId).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public  async Task<Comments> GetCommentModelById(int commentId)
        {
          
            try
            {
                return await context.StockComments.FindAsync(commentId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<JsonResponse> CreateUpdateComments(CommentSaveModel commentModel)
        {
            JsonResponse res = new JsonResponse();
            try
            {
                Comments commentObj = new Comments();
                if (commentModel.CommentId > 0)
                {

                    commentObj = await GetCommentModelById(commentModel.CommentId);
                    if (commentObj != null)
                    {
                        commentObj.Title = commentModel.Title;
                        commentObj.Content = commentModel.Content;
                        commentObj.UpdatedDate = DateTime.Now;
                        commentObj.UpdatedBy = "1";

                        context.Update(commentObj);
                        await context.SaveChangesAsync();

                        res.Status = (int)APISTATUS.OK;
                        res.StatusMessage = "records updated successfully.";

                    }
                    else
                    {

                        res.Status = (int)APISTATUS.NotFound;
                        res.StatusMessage = "no records found.";
                    }
                }

                else
                {
                    commentObj = new Comments()
                    {
                        Title = commentModel.Title,
                        Content = commentModel.Content,
                        StockId = commentModel.StockId,
                        CreatedBy = "1",
                        UpdatedBy = "1",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };

                    context.Update(commentObj);
                    await context.SaveChangesAsync();

                    res.Status = (int)APISTATUS.OK;
                    res.StatusMessage = "records created successfully.";
                }
            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.CommonMethods.GetException(ex);
            }
          

            return res;
        }

        public async Task<JsonResponse > DeleteStockComment(int stockId,int commentId)
        {
            JsonResponse res = new JsonResponse();
            try
            {
                var commentObj = await context.StockComments.FirstOrDefaultAsync(s => s.StockId == stockId && s.CommentId == commentId);
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

       

    }
}
