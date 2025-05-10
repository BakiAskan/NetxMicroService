namespace DataAcessLayer;
public interface ICommands
{
    public Task<IResultMessages<IEnumerable<dynamic>>> CommandProcList(string ProcName, string SqlParams);
    public Task<IResultMessages<dynamic>> CommandProcFirstOrDefault(string ProcName, string SqlParams);
    public Task<IResultMessages<int>> CommandProcExecute(string ProcName, string SqlParams);

}