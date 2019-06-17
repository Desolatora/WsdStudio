using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Elements
{
    public class PosVectorElement : IFeatureElement
    {
        public string DisplayName => "PosVector";
        public string ArffAttributeName => "P_VEC";
        public FeatureValueType FeatureType => FeatureValueType.Numeric;

        public int GetValueCount(FeatureSelectionContext context)
        {
            return context.FilteredPosList.Count;
        }

        public IList<FeatureValue> GetValues(RawWordEncounter encounter, FeatureSelectionContext context)
        {
            var posVector = context.FilteredPosList.GetVector(encounter.Pos);

            return FeatureValue.NewArray(posVector);
        }
    }
}
