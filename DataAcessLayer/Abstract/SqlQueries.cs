using Dapper;
using System.Data;

namespace DataAcessLayer
{
    public class SqlQueries(IDbConnection _dbConnection) : ISqlQueries
    {
        public async Task<IResultMessages<IEnumerable<dynamic>>> ExecuteQueryProc(string ProcName,string SqlParams)
        {
            return ResultMessages<IEnumerable<dynamic>>.SuccessMessage(await _dbConnection.QueryAsync<dynamic>("EXEC" + ProcName + " " + SqlParams + " "), System.Net.HttpStatusCode.OK, null);
        }
        public async Task<IResultMessages<dynamic>> QueryFirstOrDefault(string ParamSQL)
        {
            return ResultMessages<dynamic>.SuccessMessage(await _dbConnection.QueryFirstAsync<dynamic>(ParamSQL), System.Net.HttpStatusCode.OK, null);
        }

        public async Task<IResultMessages<IEnumerable<dynamic>>> QueryGetAll(string ParamSQL)
        {
            return ResultMessages<IEnumerable<dynamic>>.SuccessMessage(await _dbConnection.QueryAsync<dynamic>(ParamSQL), System.Net.HttpStatusCode.OK, null);
        }
    }
}
