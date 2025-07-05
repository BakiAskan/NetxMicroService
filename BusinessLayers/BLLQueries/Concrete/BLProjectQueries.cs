using DataAcessLayer.Queries.Projects;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayers.BLLQueries.Concrete
{
    public class BLProjectQueries(IStringLocalizerFactory factory, IProjectQueries queries) : IBLProjectQueries
    {
        private readonly IStringLocalizer _localizer = factory.Create("Messages", "BusinessLayers");
        public async Task<IResultMessages<dynamic>> ProjectById(string ParamSql)
        {
            return await queries.ProjectById(ParamSql);

        }

        public async Task<IResultMessages<IEnumerable<dynamic>>> ProjectGetAll(string ParamSql)
        {
            var result = await queries.ProjectGetAll(ParamSql);
            if (result.DataResult.Count() == 0)
            {
                result.Messages = [_localizer["VeriBulanamadi"]];
            }
            return result;
        }
    }
}
