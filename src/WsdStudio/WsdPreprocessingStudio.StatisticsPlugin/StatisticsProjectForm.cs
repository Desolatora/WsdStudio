using System.Windows.Forms;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Plugins;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.StatisticsPlugin
{
    public class StatisticsProjectForm : IPluginProjectForm
    {
        public string DisplayName => "Statistics";

        public Form CreateForm(
            IPluginComponent[] pluginComponents, WsdProject project, IProgressHandleFactory factory)
        {
            return new StatisticsForm(project);
        }
    }
}
