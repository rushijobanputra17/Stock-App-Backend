using Core.Helpers;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IStockRepository
    {
         Task<JsonResponse> GetAllStocks(QueryParameters QueryParameters);

        Task<JsonResponse> GetStocksById(int id);

        Task<JsonResponse> AddEditStockDetails(StockCreateModel stock,UserInformation user);

        Task<JsonResponse> DeleteById(int id);
        Task<List<Comments>> GetAllStockComments();

        Task<List<Comments>> GetCommentsByStockId(int stockId);

        Task<JsonResponse> CreateUpdateComments(CommentSaveModel commentModel);

        Task<JsonResponse> DeleteStockComment(int stockId, int commentId);



    }
}
