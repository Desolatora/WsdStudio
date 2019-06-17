using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace WsdPreprocessingStudio.Core.Helpers
{
    public static class DialogEx
    {
        public static OpenFileDialog OpenFile(string filter = null)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                CheckPathExists = true,
                SupportMultiDottedExtensions = true
            };

            if (filter != null)
                dialog.Filter = filter;

            return dialog;
        }

        public static SaveFileDialog SaveFile(string filter = null, string defaultExt = null)
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                CheckPathExists = true,
                SupportMultiDottedExtensions = true,
                OverwritePrompt = true,
                AddExtension = true,
                DefaultExt = defaultExt
            };

            if (filter != null)
                dialog.Filter = filter;

            return dialog;
        }

        public static CommonOpenFileDialog SelectFolder()
        {
            return new CommonOpenFileDialog
            {
                Multiselect = false,
                IsFolderPicker = true,
                Title = "Select folder",
                EnsurePathExists = true
            };
        }
    }
}
