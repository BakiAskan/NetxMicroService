using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayers.BLLQueries.Abstract
{
    public interface IBLProjectQueries
    {
        public Task<IResultMessages<IEnumerable<dynamic>>> ProjectGetAll(string ParamSql);
        public Task<IResultMessages<dynamic>> ProjectById(string ParamSql);

    }
}
