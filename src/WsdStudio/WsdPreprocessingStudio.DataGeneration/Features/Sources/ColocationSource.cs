using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Sources
{
    public class ColocationSource : IFeatureSource
    {
        public string DisplayName => "Colocation";
        public string ArffAttributeName => "COL";
        public bool RequiresAggregation => true;

        public int GetTupleCount(FeatureSelectionContext context)
        {
            return context.GenerationInfo.LeftContext +
                   context.GenerationInfo.RightContext - 1;
        }

        public IList<IList<RawWordEncounter>> GetTuples(RawRecord record, FeatureSelectionContext context)
        {
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
