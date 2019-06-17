using System;

namespace WsdPreprocessingStudio.Core.Threading
{
    public class ProgressHandleScope : IDisposable
    {
        private IProgressHandle _progress;
        private long _max;

        public ProgressHandleScope(
            IProgressHandle progress, long max, Func<long, long, string> messageFormat = null)
        {
            _progress = progress;
            _max = max;

            if (messageFormat != null)
                _progress.SetMessageFormat(messageFormat);

            _progress.Restart(max);
        }

        public bool TrySet(long current)
        {
            return _progress.TrySet(current, _max);
        }

        public void Dispose()
        {
            _progress.Complete(_max);
        }
    }
}
