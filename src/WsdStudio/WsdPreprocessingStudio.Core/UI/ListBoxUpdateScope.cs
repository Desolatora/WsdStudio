using System;
using System.Windows.Forms;

namespace WsdPreprocessingStudio.Core.UI
{
    public class ListBoxUpdateScope : IDisposable
    {
        private ListBox _listBox;

        public ListBoxUpdateScope(ListBox listBox)
        {
            _listBox = listBox ?? throw new ArgumentNullException(nameof(listBox));
            _listBox.BeginUpdate();
        }

        public void Dispose()
        {
            _listBox?.EndUpdate();
            _listBox = null;
        }
    }

    public static class ListBoxUpdateScopeExtensions
    {
        public static ListBoxUpdateScope UpdateScope(this ListBox listBox)
        {
            return new ListBoxUpdateScope(listBox);
        }
    }
}
