using DataAcessLayer.Queries.Companies;
using DataAcessLayer.Queries.Stocks;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayers.BLLQueries.Concrete
{
    public class BLCompanyQueries(IStringLocalizerFactory factory, ICompanyQueries queries) : IBLCompanyQueries
    {
        private readonly IStringLocalizer _localizer = factory.Create("Messages", "BusinessLayers");
        public async Task<IResultMessages<dynamic>> CompanyById(string ParamSql)
        {
            return await queries.CompanyById(ParamSql);
           
        }

        public async Task<IResultMessages<IEnumerable<dynamic>>> CompanyGetAll(string ParamSql)
        {
            var result = await queries.CompanyGetAll(ParamSql);
            if (result.DataResult.Count() == 0)
            {
                result.Messages = [_localizer["VeriBulanamadi"]];
            }
            return result;
        }
    }
}
