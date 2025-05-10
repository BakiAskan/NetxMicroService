using BusinessLayers.BLLQueries.Abstract;
using DataAcessLayer.Queries.Orders;

namespace BusinessLayers.BLLQueries
{
    [AOPException]
    [AOPLog]
    public class BLOrderQueries(IOrderQueries order) : IBLOrderQueries
    {
        public async Task<IResultMessages<dynamic>> OrderById(string Params)
        {
            return await order.OrderById(Params);
        }

   
        public async Task<IResultMessages<IEnumerable<dynamic>>> OrderGetAll(string SQLString)
        {
            if (SQLString == "")
            {
                return await order.OrderGetAll(SQLString);
            }
            else
            {
                return await order.OrderGetAll(SQLString);
            }
        }
    }
}
