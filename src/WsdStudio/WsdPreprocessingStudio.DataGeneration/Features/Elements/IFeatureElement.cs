using System.Collections.Generic;
using Newtonsoft.Json;
using WsdPreprocessingStudio.Core.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Elements
{
    public interface IFeatureElement
    {
        [JsonIgnore]
        string DisplayName { get; }

        [JsonIgnore]
        string ArffAttributeName { get; }

        [JsonIgnore]
        FeatureValueType FeatureType { get; }

        int GetValueCount(FeatureSelectionContext context);
        IList<FeatureValue> GetValues(RawWordEncounter encounter, FeatureSelectionContext context);
    }
}
