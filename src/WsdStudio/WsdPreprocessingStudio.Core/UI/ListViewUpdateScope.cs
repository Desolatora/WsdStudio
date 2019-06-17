using System;
using System.Windows.Forms;

namespace WsdPreprocessingStudio.Core.UI
{
    public class ListViewUpdateScope : IDisposable
    {
        private ListView _listView;

        public ListViewUpdateScope(ListView listView)
        {
            _listView = listView ?? throw new ArgumentNullException(nameof(listView));
            _listView.BeginUpdate();
        }

        public void Dispose()
        {
            _listView?.EndUpdate();
            _listView = null;
        }
    }

    public static class ListViewUpdateScopeExtensions
    {
        public static ListViewUpdateScope UpdateScope(this ListView listView)
        {
            return new ListViewUpdateScope(listView);
        }
    }
}
