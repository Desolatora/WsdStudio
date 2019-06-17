using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using WsdPreprocessingStudio.Core.Extensions;
using WsdPreprocessingStudio.Core.Threading;
using WsdPreprocessingStudio.Forms;

namespace WsdPreprocessingStudio.UI
{
    public class ProgressHandle : IProgressHandle
    {
        public uint MinUpdateIntervalMilliseconds { get; set; } = 250;

        private Control _owner;
        private ProgressForm _form;
        private TimeSpan _lastUpdateTime;
        private Stopwatch _stopwatch;
        private Func<long, long, string> _labelFormatFunc;
        private CancellationTokenSource _cancellation;

        public ProgressHandle(string title, Control owner)
        {
            _cancellation = new CancellationTokenSource();
            _lastUpdateTime = TimeSpan.Zero;
            _labelFormatFunc = (cur, max) => $"{cur} of {max}";
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            _form = new ProgressForm(title);
            _form.CancelOperationButton.Click += (sender, args) =>
            {
                _cancellation.Cancel();
                _form.CancelOperationButton.Enabled = false;
            };
            _owner.InvokeIfRequired(() =>
            {
                _form.Show(owner);
            });
        }

        public void SetMessageFormat(Func<long, long, string> messageFormat)
        {
            _labelFormatFunc = messageFormat ?? throw new ArgumentNullException(nameof(messageFormat));
        }

        public void Restart(long max)
        {
            max = Math.Max(max, 1);

            _owner.InvokeIfRequired(() =>
            {
                SetProgressPrivate(0, max);
            });
        }

        public bool TrySet(long current, long max)
        {
            max = Math.Max(max, 1);
            current = Math.Max(current, 0);
            current = Math.Min(current, max);
            
            var elapsed = _stopwatch.Elapsed;

            if ((elapsed - _lastUpdateTime).TotalMilliseconds < MinUpdateIntervalMilliseconds)
                return false;

            _owner.InvokeIfRequired(() =>
            {
                SetProgressPrivate(current, max);
            });

            _lastUpdateTime = elapsed;

            return true;
        }

        public void Complete(long max)
        {
            max = Math.Max(max, 1);

            _owner.InvokeIfRequired(() =>
            {
                SetProgressPrivate(max, max);
            });
        }

        public ProgressHandleScope Scope(long max, Func<long, long, string> labelFormat = null)
        {
            return new ProgressHandleScope(this, max, labelFormat);
        }

        private void SetProgressPrivate(long current, long max)
        {
            _cancellation.Token.ThrowIfCancellationRequested();

            var percent = current / (double) max;

            _form.ProgressLabel.Text = _labelFormatFunc.Invoke(current, max);
            _form.ProgressBar.Value = (int) ((_form.ProgressBar.Maximum - _form.ProgressBar.Minimum) * percent) +
                                      _form.ProgressBar.Minimum;
        }

        public void Dispose()
        {
            _owner.InvokeIfRequired(() =>
            {
                _form?.Dispose();
                _form = null;
            });

            _cancellation?.Dispose();
            _cancellation = null;
        }
    }
}