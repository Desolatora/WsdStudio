using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Elements
{
    public class WordElement : IFeatureElement
    {
        public string DisplayName => "Word";
        public string ArffAttributeName => "W_TXT";
        public FeatureValueType FeatureType => FeatureValueType.String;

        public int GetValueCount(FeatureSelectionContext context)
        {
            return 1;
        }

        public IList<FeatureValue> GetValues(RawWordEncounter encounter, FeatureSelectionContext context)
        {
            return new[] {new FeatureValue(encounter.Word)};
        }
    }
}
