using System;

namespace WsdPreprocessingStudio.Core.Threading
{
    public interface IProgressHandle : IDisposable
    {
        bool ThrottleRestartAndComplete { get; set; }

        void SetMessageFormat(Func<long, long, string> messageFormat);
        void Restart(long max);
        bool TrySet(long current, long max);
        void Complete(long max);
        ProgressHandleScope Scope(long max, Func<long, long, string> messageFormat = null);
    }
}
