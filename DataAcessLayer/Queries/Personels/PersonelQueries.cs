namespace DataAcessLayer.Queries.Personels
{
    public class PersonelQueries(ISqlQueries queries) : IPersonelQueries
    {
        public async Task<IResultMessages<dynamic>> PersonelById(string ParamSql)
        {
            return await queries.QueryFirstOrDefault(ParamSql);
        }

        public async Task<IResultMessages<IEnumerable<dynamic>>> PersonelFilter(string ParamFilters)
        {
            return await queries.QueryGetAll(ParamFilters);
        }

        public async Task<IResultMessages<dynamic>> PersonelLogin(string Username, string Passwords)
        {
            return await queries.QueryFirstOrDefault("SELECT * FROM ppersonel where sifre = '"+ Passwords + "' and perkodu = '"+ Username + "'");
        }

        public async Task<IResultMessages<IEnumerable<dynamic>>> ProsedurCalistir(string ProcName, string SqlParams)
        {
            return await queries.ExecuteQueryProc(ProcName, SqlParams);
        }
    }
}
