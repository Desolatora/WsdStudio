using WsdPreprocessingStudio.Core.Plugins;

namespace WsdPreprocessingStudio.SamplePlugin
{
    public class LoggingPlugin : IPlugin
    {
        public string DisplayName => "Sample logging plugin";

        public IPluginComponent[] GetComponents()
        {
            return new IPluginComponent[]
            {
                new LoggedColocationSource(),
                new LoggedCosThetaUnitary(),
                new LoggedStringConcat(),
                new LoggedWordElement(),
                new LoggingDataGenerationHandler(),
                new LoggingProjectForm()
            };
        }
    }
}
