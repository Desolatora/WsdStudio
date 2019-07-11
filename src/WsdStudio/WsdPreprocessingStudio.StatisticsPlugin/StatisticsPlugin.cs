using WsdPreprocessingStudio.Core.Plugins;

namespace WsdPreprocessingStudio.StatisticsPlugin
{
    public class StatisticsPlugin : IPlugin
    {
        public string DisplayName => "Statistics plugin";

        public IPluginComponent[] GetComponents()
        {
            return new IPluginComponent[]
            {
                new StatisticsProjectForm(),
                new StatisticsHandler()
            };
        }
    }
}
