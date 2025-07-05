using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayers.BLLQueries.Abstract
{
    public interface IBLCompanyQueries
    {
        public Task<IResultMessages<IEnumerable<dynamic>>> CompanyGetAll(string ParamSql);
        public Task<IResultMessages<dynamic>> CompanyById(string ParamSql);
    }
}
