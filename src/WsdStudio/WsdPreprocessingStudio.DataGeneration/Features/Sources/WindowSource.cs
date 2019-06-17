using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Sources
{
    public class WindowSource : IFeatureSource
    {
        public string DisplayName => "Window";
        public string ArffAttributeName => "WIN";
        public bool RequiresAggregation => false;

        public int GetTupleCount(FeatureSelectionContext context)
        {
            return context.GenerationInfo.LeftContext +
                   context.GenerationInfo.RightContext + 1;
        }

        public IList<IList<RawWordEncounter>> GetTuples(RawRecord record, FeatureSelectionContext context)
        {
            var result = new List<IList<RawWordEncounter>>();

            for (var i = 0; i < context.GenerationInfo.LeftContext; i++)
            {
                result.Add(new[] { record.Context[i] });
            }

            result.Add(new[] {(RawWordEncounter) record});

            for (var i = context.GenerationInfo.LeftContext; i < record.Context.Length; i++)
            {
                result.Add(new[] {record.Context[i]});
            }

            return result;
        }
    }
}
