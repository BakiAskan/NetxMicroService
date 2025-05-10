namespace DataAcessLayer
{
    public interface ISqlQueries
    {
        public Task<IResultMessages<IEnumerable<dynamic>>> QueryGetAll(string ParamSQL);
        public Task<IResultMessages<dynamic>> QueryFirstOrDefault(string ParamSQL);
        public Task<IResultMessages<IEnumerable<dynamic>>> ExecuteQueryProc(string ProcName, string SqlParams);
    }
}
