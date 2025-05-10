using Newtonsoft.Json;
using System.Data;

namespace Utilities
{
    /// <summary>
    /// eqweqwe
    /// </summary>
    public static class JsonParse
    {
        public static DataTable? ToDataTable(this string json)
        {
            return JsonConvert.DeserializeObject<DataTable>(json);
        }
    }
}
