using System.Windows.Forms;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.Plugins
{
    public interface IPluginProjectForm : IPluginComponent
    {
        Form CreateForm(IPluginComponent[] pluginComponents, WsdProject project, IProgressHandleFactory factory);
    }
}