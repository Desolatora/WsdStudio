using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Features;
using WsdPreprocessingStudio.DataGeneration.Plugins;

namespace WsdPreprocessingStudio.SamplePlugin
{
    public class LoggedColocationSource : IPluginFeatureSource
    {
        public string DisplayName => "LoggedColocation";
        public string ArffAttributeName => "COL";
        public bool RequiresAggregation => true;

        public int GetTupleCount(FeatureSelectionContext context)
        {
            return context.GenerationInfo.LeftContext +
                   context.GenerationInfo.RightContext - 1;
        }

        public IList<IList<RawWordEncounter>> GetTuples(RawRecord record, FeatureSelectionContext context)
        {
            context.Project.PluginData
                .GetData<LoggingPlugin, UsageStatistics>(string.Empty)
                .ColocationSourceCounter++;

            const int distance = 1;

            var result = new List<IList<RawWordEncounter>>();

            for (var i = 0; i < record.Context.Length - distance; i++)
            {
                result.Add(new[] {record.Context[i], record.Context[i + distance]});
            }

            return result;
        }
    }
}
