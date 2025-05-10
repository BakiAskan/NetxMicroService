using Castle.DynamicProxy;
using ErpMikroservis.GlobalModel;
using ErpMikroservis.AspectCore.LogWriting;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace ErpMikroservis.AspectCore
{
    public class PerformanceAspect : InterceptorBase<AOpPerformanceAttribute>
    {
        private readonly Stopwatch _stopwatch;
        private readonly IOptions<SettingsOptions> settings;
        private readonly ILogWriteExtensions logWirte;
        public PerformanceAspect(IOptions<SettingsOptions> _settings, ILogWriteExtensions _logWirte)
        {
            settings = _settings;
            logWirte = _logWirte;
            _stopwatch = new Stopwatch();
        }
        protected override void OnBefore(IInvocation invocation, AOpPerformanceAttribute attribute)
        {
            if (settings.Value.IsPerformance)
            {
                _stopwatch.Start();
            }
        }
        protected override void OnAfter(IInvocation invocation, AOpPerformanceAttribute attribute)
        {
            if (settings.Value.IsPerformance)
            {
                if (_stopwatch.Elapsed.TotalMilliseconds > settings.Value.PerformanceInterval)
                {
                    _stopwatch.Stop();
                    logWirte.FileLogAdd($"{invocation.Method.DeclaringType?.FullName}.{invocation.Method.Name}() elapsed {_stopwatch.Elapsed.TotalSeconds} second(s)." + "Date : " + DateTime.Now.ToString(), settings.Value.LogPathFile, "Performance");
                }
            }
        }
    }
}
