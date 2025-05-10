namespace DataAcessLayer.Queries.Orders
{
    public interface IOrderQueries
    {
        public Task<IResultMessages<IEnumerable<dynamic>>> OrderGetAll(string ParamSql);
        public Task<IResultMessages<dynamic>> OrderById(string ParamSql);

    }
}
