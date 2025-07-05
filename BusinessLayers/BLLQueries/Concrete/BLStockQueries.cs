using DataAcessLayer.Queries.Stocks;
using Microsoft.Extensions.Localization;

namespace BusinessLayers.BLLQueries.Concrete
{
    public class BLStockQueries(IStringLocalizerFactory factory,IStockQueries queries) : IBLStockQueries
    {
        private readonly IStringLocalizer _localizer = factory.Create("Messages", "BusinessLayers");
        public Task<IResultMessages<dynamic>> ItemById(string ParamSql)
        {
            throw new NotImplementedException();
        }

        public async Task<IResultMessages<IEnumerable<dynamic>>> ItemGetAll(string ParamSql)
        {
           var result = await queries.ItemGetAll(ParamSql);
            if (result.DataResult.Count() == 0)
            {
                result.Messages = [_localizer["VeriBulanamadi"]];
            }
            return result;
        }

        public Task<IResultMessages<IEnumerable<dynamic>>> ItemPackingGetAll(string ParamSql)
        {
            throw new NotImplementedException();
        }

        public Task<IResultMessages<IEnumerable<dynamic>>> ItemPriceGetAll(string ParamSql)
        {
            throw new NotImplementedException();
        }

        public Task<IResultMessages<IEnumerable<dynamic>>> ItemRecipeGetAll(string ParamSql)
        {
            throw new NotImplementedException();
        }

        public Task<IResultMessages<IEnumerable<dynamic>>> StockBalanceGetAll(string ParamSql)
        {
            throw new NotImplementedException();
        }

        public Task<IResultMessages<IEnumerable<dynamic>>> StockMovementsGetAll(string ParamSql)
        {
            throw new NotImplementedException();
        }
    }
}
