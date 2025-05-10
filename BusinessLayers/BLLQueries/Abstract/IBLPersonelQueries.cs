namespace BusinessLayers.BLLQueries.Abstract
{
    public interface IBLPersonelQueries
    {
        public Task<IResultMessages<dynamic>> PersonelLogin(RequestLogin model);
        public Task<IResultMessages<dynamic>> Deneme(RequestLogin model);
    }
}
