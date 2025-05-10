namespace ErpMikroservis.GlobalModel
{
    public record SettingsOptions
    {
        public bool IsCache { get; set; } = false;
        public bool IsPerformance { get; set; } = false;
        public bool IsLog { get; set; } = false;
        public bool IsException { get; set; } = false;
        public int CacheDuracation { get; set; } = 0;
        public int PerformanceInterval { get; set; } = 0;
        public string LogPathFile { get; set; } = "";
    }
}
