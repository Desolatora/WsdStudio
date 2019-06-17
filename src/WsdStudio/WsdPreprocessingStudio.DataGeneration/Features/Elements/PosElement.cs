using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Data.Collections;

namespace WsdPreprocessingStudio.DataGeneration.Features.Elements
{
    public class PosElement : INominalFeatureElement
    {
        public string DisplayName => "Pos";
        public string ArffAttributeName => "P_TXT";
        public FeatureValueType FeatureType => FeatureValueType.String;

        public int GetValueCount(FeatureSelectionContext context)
        {
            return 1;
        }

        public IList<FeatureValue> GetValues(RawWordEncounter encounter, FeatureSelectionContext context)
        {
            return new[] {new FeatureValue(context.FilteredPosList.GetOrDefault(encounter.Pos))};
        }

        public IList<FeatureValue> GetNominalValues(FeatureSelectionContext context)
        {
            return context.FilteredPosList
                .Union(new[] {WsdPosList.EmptyPos})
                .Select(x => new FeatureValue(x))
                .ToArray();
        }
    }
}
