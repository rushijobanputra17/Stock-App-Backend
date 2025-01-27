using Core.Data;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.Enums;

namespace Core.Repository
{
    public class CompanyRepository : IComapnyRepository
    {
        private ApplicationDbContext context { get; set; }

        public CompanyRepository(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        public async Task<JsonResponse> GetAllCompainines()
        {
            JsonResponse res = new JsonResponse();
            try
            {
                var companinie =  await context.Companys.ToListAsync();
                if (companinie!=null && companinie.Any())
                {
                    res.Data = companinie;
                    res.Status = (int)APISTATUS.OK;
                }
                else
                {
                    res.Status = (int)APISTATUS.NotFound;
                    res.StatusMessage = "No records found";
                }
            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.CommonMethods.GetException(ex);
            }
            return res;
        }
        
        public async Task<Company> GetCompanyModel(int companyId)
        {
            return await context.Companys.FirstOrDefaultAsync(x=>x.CompanyId==companyId);
        }

        public async Task<JsonResponse> InsertUpdateCompainies(AddCompanyModel model,UserInformation user)
        {
            JsonResponse res = new JsonResponse();
            user = user ?? new UserInformation();
            try
            {
                if (model.CompanyId>0)
                {
                    var companyObj = await GetCompanyModel(model.CompanyId);
                    if (companyObj != null)
                    {
                        companyObj.CompanyName = model.CompanyName;
                        companyObj.UpdatedBy = user.UserId;
                        companyObj.UpdatedDate = DateTime.Now;

                        context.Companys.Update(companyObj);
                        await context.SaveChangesAsync();
                        res.Status = (int)APISTATUS.OK;
                    }
                    else
                    {
                        res.Status = (int)APISTATUS.NotFound;
                        res.StatusMessage = "Invalid Data";
                    }
                }
                else
                {
                    var companyObj = new Company()
                    {
                        CompanyId = 0,
                        CompanyName = model.CompanyName,
                        CreatedBy=user.UserId,
                        UpdatedBy =user.UserId,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };

                    context.Companys.Add(companyObj);
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

        public async Task<JsonResponse> DeleteCompanyById(int id)
        {
            JsonResponse res = new JsonResponse();
            try
            {
                var companyObj = await GetCompanyModel(id);
                if (companyObj != null)
                {
                     context.Companys.Remove(companyObj);
                    await context.SaveChangesAsync();
                    res.Status = (int)APISTATUS.OK;
                }
                else
                {
                    res.Status = (int)APISTATUS.NotFound;
                    res.StatusMessage = "No records found";
                }
            }
            catch (Exception ex)
            {
                res.Status = (int)APISTATUS.InternalServerError;
                res.StatusMessage = CommonMethods.CommonMethods.GetException(ex);
            }
            return res;
        }


        public async Task<JsonResponse> GetCompanyById(int id)
        {
            JsonResponse res = new JsonResponse();
            try
            {
                var companyObj = await GetCompanyModel(id);
                if (companyObj != null)
                {
                  
                    res.Status = (int)APISTATUS.OK;
                    res.Data = companyObj;
                }
                else
                {
                    res.Status = (int)APISTATUS.NotFound;
                    res.StatusMessage = "No records found";
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
