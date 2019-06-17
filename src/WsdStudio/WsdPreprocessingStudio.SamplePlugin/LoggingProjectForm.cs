using System.Windows.Forms;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Plugins;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.SamplePlugin
{
    public class LoggingProjectForm : IPluginProjectForm
    {
        public string DisplayName => "Logging";

        public Form CreateForm(
            IPluginComponent[] pluginComponents, WsdProject project, IProgressHandleFactory factory)
        {
            return new LoggingForm(project);
        }
    }
}
