using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Sources
{
    public class ContextSource : IFeatureSource
    {
        public string DisplayName => "Context";
        public string ArffAttributeName => "CTX";
        public bool RequiresAggregation => false;

        public int GetTupleCount(FeatureSelectionContext context)
        {
            return context.GenerationInfo.LeftContext +
                   context.GenerationInfo.RightContext;
        }

        public IList<IList<RawWordEncounter>> GetTuples(RawRecord record, FeatureSelectionContext context)
        {
            var result = new List<IList<RawWordEncounter>>();

            for (var i = 0; i < record.Context.Length; i++)
            {
                result.Add(new[] {record.Context[i]});
            }

            return result;
        }
    }
}
