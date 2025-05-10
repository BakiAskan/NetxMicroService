
namespace DataAcessLayer.Queries.Orders
{
    public class OrderQueries(ISqlQueries sqlQueries) : IOrderQueries
    {
        public async Task<IResultMessages<dynamic>> OrderById(string ParamSql)
        {
            return await sqlQueries.QueryFirstOrDefault(QueriesGet.OrderGetAll + " WHERE " + ParamSql);
        }

        public async Task<IResultMessages<IEnumerable<dynamic>>> OrderGetAll(string ParamSql)
        {
            if (ParamSql == "")
            {
                return await sqlQueries.QueryGetAll(QueriesGet.OrderGetAll);
            }
            return await sqlQueries.QueryGetAll(QueriesGet.OrderGetAll + " WHERE " + ParamSql);
        }
    }
}
