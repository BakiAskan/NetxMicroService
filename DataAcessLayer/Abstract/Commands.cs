namespace DataAcessLayer
{
    public class Commands(IDbConnection _dbConnection) : ICommands
    {
        public async Task<IResultMessages<int>> CommandProcExecute(string ProcName, string SqlParams)
        {
            return ResultMessages<int>.SuccessMessage(await _dbConnection.ExecuteAsync("EXEC" + ProcName + " " + SqlParams + " "), HttpStatusCode.OK);
        }
        public async Task<IResultMessages<dynamic>> CommandProcFirstOrDefault(string ProcName, string SqlParams)
        {
            return ResultMessages<dynamic>.SuccessMessage(await _dbConnection.QueryFirstOrDefault<dynamic>("EXEC" + ProcName + " " + SqlParams + " "), HttpStatusCode.OK);
        }
        public async Task<IResultMessages<IEnumerable<dynamic>>> CommandProcList(string ProcName, string SqlParams)
        {
            return ResultMessages<IEnumerable<dynamic>>.SuccessMessage(await _dbConnection.QueryAsync<dynamic>("EXEC" + ProcName + " " + SqlParams + " "), HttpStatusCode.OK);
        }
        public async Task<IResultMessages<dynamic>> CommandProcExecuteReturnValue(string SqlParams)
        {
            return ResultMessages<dynamic>.SuccessMessage(await _dbConnection.ExecuteScalarAsync(SqlParams), HttpStatusCode.OK);
        }
    }
}
