using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Queries.Projects
{
    public class ProjectQueries(ISqlQueries sqlQueries) : IProjectQueries
    {
        public async Task<IResultMessages<dynamic>> ProjectById(string ParamSql)
        {
            return await sqlQueries.QueryFirstOrDefault("SELECT * FROM NV_ProjectDetail" + ParamSql + "");
        }

        public async Task<IResultMessages<IEnumerable<dynamic>>> ProjectGetAll(string ParamSql)
        {
            if (ParamSql != "")
            {
                ParamSql = "WHERE " + ParamSql;
            }
            return await sqlQueries.QueryGetAll("SELECT * FROM NV_ProjectDetail " + ParamSql + "");
        }
    }
}
