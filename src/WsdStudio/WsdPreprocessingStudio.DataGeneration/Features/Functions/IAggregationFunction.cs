using System.Collections.Generic;
using Newtonsoft.Json;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Functions
{
    public interface IAggregationFunction
    {
        [JsonIgnore]
        string DisplayName { get; }

        [JsonIgnore]
        IList<FeatureValueType> SupportedFeatureTypes { get; }

        IList<FeatureValue> Aggregate(
            IList<EncounterValues> values, RawRecord record,
            FeatureGroup featureGroup, FeatureSelectionContext context);
    }
}
