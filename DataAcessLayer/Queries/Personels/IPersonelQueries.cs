namespace DataAcessLayer.Queries.Personels
{
    public interface IPersonelQueries
    {
        public Task<IResultMessages<dynamic>> PersonelById(string ParamSql);
        public Task<IResultMessages<IEnumerable<dynamic>>> PersonelFilter(string ParamFilters);
        public Task<IResultMessages<dynamic>> PersonelLogin(string Username,string Passwords);
    }
}
