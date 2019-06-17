using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.DataGeneration.Features;
using WsdPreprocessingStudio.DataGeneration.Plugins;

namespace WsdPreprocessingStudio.SamplePlugin
{
    public class LoggedWordElement : IPluginFeatureElement
    {
        public string DisplayName => "LoggedWord";
        public string ArffAttributeName => "W_TXT";
        public FeatureValueType FeatureType => FeatureValueType.String;

        public int GetValueCount(FeatureSelectionContext context)
        {
            return 1;
        }

        public IList<FeatureValue> GetValues(RawWordEncounter encounter, FeatureSelectionContext context)
        {
            context.Project.PluginData
                .GetData<LoggingPlugin, UsageStatistics>(string.Empty)
                .WordElementCounter++;

            return new[] {new FeatureValue(encounter.Word)};
        }
    }
}
