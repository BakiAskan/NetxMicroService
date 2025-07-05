using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Queries.Stocks
{
    public class StockQueries(ISqlQueries sqlQueries) : IStockQueries
    {
        public Task<IResultMessages<dynamic>> ItemById(string ParamSql)
        {
            throw new NotImplementedException();
        }

        public async Task<IResultMessages<IEnumerable<dynamic>>> ItemGetAll(string ParamSql)
        {
            if (ParamSql != "")
            {
                ParamSql = "WHERE " + ParamSql;
            }
            return await sqlQueries.QueryGetAll("SELECT * FROM NV_ItemList " + ParamSql + "");
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
