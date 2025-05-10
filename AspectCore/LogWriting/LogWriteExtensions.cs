namespace ErpMikroservis.AspectCore.LogWriting
{
    public class LogWriteExtensions : ILogWriteExtensions
    {
        public async Task FileLogAdd(string LogText, string LogPathFile, string LogType)
        {
            string Path = LogPathFile + LogType + "-" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            using (StreamWriter sw = (File.Exists(Path)) ? File.AppendText(Path) : File.CreateText(Path))
            {
                sw.WriteLine(LogText);
                sw.Close();
            }
        }
    }
}
