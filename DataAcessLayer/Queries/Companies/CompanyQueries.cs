using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Queries.Companies
{
    public class CompanyQueries(ISqlQueries sqlQueries) : ICompanyQueries
    {
        public async Task<IResultMessages<dynamic>> CompanyById(string ParamSql)
        {
            return await sqlQueries.QueryFirstOrDefault("SELECT * FROM NV_Companies " + ParamSql + "");
        }

        public async Task<IResultMessages<IEnumerable<dynamic>>> CompanyGetAll(string ParamSql)
        {
            if (ParamSql != "")
            {
                ParamSql = "WHERE " + ParamSql;
            }
            return await sqlQueries.QueryGetAll("SELECT * FROM NV_Companies " + ParamSql + "");
        }
    }
}
