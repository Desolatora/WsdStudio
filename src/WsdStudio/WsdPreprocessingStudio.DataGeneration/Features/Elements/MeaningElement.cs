using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Elements
{
    public class MeaningElement : IFeatureElement
    {
        public string DisplayName => "Meaning";
        public string ArffAttributeName => "M_TXT";
        public FeatureValueType FeatureType => FeatureValueType.String;

        public int GetValueCount(FeatureSelectionContext context)
        {
            return 1;
        }

        public IList<FeatureValue> GetValues(RawWordEncounter encounter, FeatureSelectionContext context)
        {
            return new[]
            {
                new FeatureValue(
                    !string.IsNullOrEmpty(encounter.Meaning)
                        ? encounter.Meaning
                        : "unknown")
            };
        }
    }
}
