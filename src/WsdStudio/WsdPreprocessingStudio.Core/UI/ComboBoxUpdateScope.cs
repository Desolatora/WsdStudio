using System;
using System.Windows.Forms;

namespace WsdPreprocessingStudio.Core.UI
{
    public class ComboBoxUpdateScope : IDisposable
    {
        private ComboBox _comboBox;

        public ComboBoxUpdateScope(ComboBox comboBox)
        {
            _comboBox = comboBox ?? throw new ArgumentNullException(nameof(comboBox));
            _comboBox.BeginUpdate();
        }

        public void Dispose()
        {
            _comboBox?.EndUpdate();
            _comboBox = null;
        }
    }

    public static class ComboBoxUpdateScopeExtensions
    {
        public static ComboBoxUpdateScope UpdateScope(this ComboBox comboBox)
        {
            return new ComboBoxUpdateScope(comboBox);
        }
    }
}
