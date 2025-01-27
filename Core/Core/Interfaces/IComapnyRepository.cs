using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public  interface IComapnyRepository
    {
        Task<JsonResponse> GetAllCompainines();


        Task<JsonResponse> InsertUpdateCompainies(AddCompanyModel model, UserInformation user);
        Task<JsonResponse> DeleteCompanyById(int id);

        Task<JsonResponse> GetCompanyById(int id);

    }
}
