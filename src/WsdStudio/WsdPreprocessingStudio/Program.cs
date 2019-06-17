using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WsdPreprocessingStudio.Forms;

namespace WsdPreprocessingStudio
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.ThreadException += (sender, args) =>
            {
                MessageBox.Show(args.Exception.ToString(), "Error");
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                MessageBox.Show(args.ExceptionObject.ToString(), "Error");
            };
            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                MessageBox.Show(args.Exception.ToString(), "Error");
            };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
