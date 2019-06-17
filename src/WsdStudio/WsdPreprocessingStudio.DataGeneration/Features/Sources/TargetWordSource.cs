using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Sources
{
    public class TargetWordSource : IFeatureSource
    {
        public string DisplayName => "TargetWord";
        public string ArffAttributeName => "TW";
        public bool RequiresAggregation => false;

        public int GetTupleCount(FeatureSelectionContext context)
        {
            return 1;
        }

        public IList<IList<RawWordEncounter>> GetTuples(RawRecord record, FeatureSelectionContext context)
        {
            return new[] {(IList<RawWordEncounter>) new[] {(RawWordEncounter) record}};
        }
    }
}
