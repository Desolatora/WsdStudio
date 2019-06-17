using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Functions
{
    public class StringConcat : IAggregationFunction
    {
        public string DisplayName => "StringConcat(x, '__', y)";
        public IList<FeatureValueType> SupportedFeatureTypes => new[] {FeatureValueType.String};

        public IList<FeatureValue> Aggregate(
            IList<EncounterValues> values, RawRecord record, 
            FeatureGroup featureGroup, FeatureSelectionContext context)
        {
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
