using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Elements
{
    public class WordIdElement : IFeatureElement
    {
        public string DisplayName => "WordId";
        public string ArffAttributeName => "W_ID";
        public FeatureValueType FeatureType => FeatureValueType.Numeric;

        public int GetValueCount(FeatureSelectionContext context)
        {
            return 1;
        }

        public IList<FeatureValue> GetValues(RawWordEncounter encounter, FeatureSelectionContext context)
        {
            return new[] {new FeatureValue(context.ReorderedDictionary.GetByName(encounter.Word)?.Id ?? 0)};
        }
    }
}
