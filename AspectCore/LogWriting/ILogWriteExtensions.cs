namespace ErpMikroservis.AspectCore.LogWriting
{
    public interface ILogWriteExtensions
    {
        public Task FileLogAdd(string LogText,string LogPathFile,string LogType);
    }
}
