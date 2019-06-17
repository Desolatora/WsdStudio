using System.Windows.Forms;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.UI
{
    public class ProgressHandleFactory : IProgressHandleFactory
    {
        private Control _owner;

        public ProgressHandleFactory(Control owner)
        {
            _owner = owner;
        }

        public IProgressHandle NewInstance(string title)
        {
            return new ProgressHandle(title, _owner);
        }
    }
}
