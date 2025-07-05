using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Queries.Projects
{
    public interface IProjectQueries
    {
        public Task<IResultMessages<IEnumerable<dynamic>>> ProjectGetAll(string ParamSql);
        public Task<IResultMessages<dynamic>> ProjectById(string ParamSql);
    }
}
