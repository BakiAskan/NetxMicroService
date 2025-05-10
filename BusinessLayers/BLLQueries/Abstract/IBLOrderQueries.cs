namespace BusinessLayers.BLLQueries.Abstract
{
    public interface IBLOrderQueries
    {
        public Task<IResultMessages<IEnumerable<dynamic>>> OrderGetAll(string SQLString);
        public Task<IResultMessages<dynamic>> OrderById(string Params);
    }
}
