using System.Collections.Generic;
using Newtonsoft.Json;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Sources
{
    public interface IFeatureSource
    {
        [JsonIgnore]
        string DisplayName { get; }

        [JsonIgnore]
        string ArffAttributeName { get; }

        [JsonIgnore]
        bool RequiresAggregation { get; }

        int GetTupleCount(FeatureSelectionContext context);
        IList<IList<RawWordEncounter>> GetTuples(RawRecord record, FeatureSelectionContext context);
    }
}
