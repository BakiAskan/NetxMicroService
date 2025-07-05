using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Queries.Companies
{
    public interface ICompanyQueries
    {
        public Task<IResultMessages<IEnumerable<dynamic>>> CompanyGetAll(string ParamSql);
        public Task<IResultMessages<dynamic>> CompanyById(string ParamSql);
    }
}
