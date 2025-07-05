using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Queries.Stocks
{
    public interface IStockQueries
    {
        public Task<IResultMessages<IEnumerable<dynamic>>> ItemGetAll(string ParamSql);
        public Task<IResultMessages<dynamic>> ItemById(string ParamSql);
        public Task<IResultMessages<IEnumerable<dynamic>>> ItemPriceGetAll(string ParamSql);
        public Task<IResultMessages<IEnumerable<dynamic>>> ItemRecipeGetAll(string ParamSql);
        public Task<IResultMessages<IEnumerable<dynamic>>> ItemPackingGetAll(string ParamSql);
        public Task<IResultMessages<IEnumerable<dynamic>>> StockBalanceGetAll(string ParamSql);
        public Task<IResultMessages<IEnumerable<dynamic>>> StockMovementsGetAll(string ParamSql);
        
    }
}
