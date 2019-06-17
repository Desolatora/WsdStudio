using System.Collections.Generic;
using Newtonsoft.Json;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Functions
{
    public interface ICompressionFunction
    {
        [JsonIgnore]
        string DisplayName { get; }

        [JsonIgnore]
        IList<FeatureValueType> SupportedFeatureTypes { get; }

        [JsonIgnore]
        bool RequiresCompressionElements { get; }

        FeatureValue Compress(
            IList<FeatureValue> values, RawRecord record, 
            FeatureGroup featureGroup, FeatureSelectionContext context);
    }
}
