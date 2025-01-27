using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<JsonResponse> GetAll(string userId);

        Task<JsonResponse> DeleltePortfolio(int portfolioId);

        Task<JsonResponse> AddPortfolio(Portfolio model, UserInformation user);

    }
}
