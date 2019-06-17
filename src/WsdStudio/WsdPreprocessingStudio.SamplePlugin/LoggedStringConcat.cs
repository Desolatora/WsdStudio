using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Features;
using WsdPreprocessingStudio.DataGeneration.Plugins;

namespace WsdPreprocessingStudio.SamplePlugin
{
    public class LoggedStringConcat : IPluginAggregationFunction
    {
        public string DisplayName => "LoggedStringConcat(x, '__', y)";
        public IList<FeatureValueType> SupportedFeatureTypes => new[] {FeatureValueType.String};

        public IList<FeatureValue> Aggregate(
            IList<EncounterValues> values, RawRecord record, 
            FeatureGroup featureGroup, FeatureSelectionContext context)
        {
            context.Project.PluginData
                .GetData<LoggingPlugin, UsageStatistics>(string.Empty)
                .StringConcatCounter++;

            var result = new FeatureValue[values[0].Values.Count];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = new FeatureValue(
                    string.Join("__", values.Select(x => x.Values[i].StringValue)));
            }

            return result;
        }
    }
}
