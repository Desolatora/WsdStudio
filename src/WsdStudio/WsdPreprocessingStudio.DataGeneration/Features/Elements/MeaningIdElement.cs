using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Elements
{
    public class MeaningIdElement : INominalFeatureElement
    {
        public string DisplayName => "MeaningId";
        public string ArffAttributeName => "M_ID";
        public FeatureValueType FeatureType => FeatureValueType.Numeric;

        public int GetValueCount(FeatureSelectionContext context)
        {
            return 1;
        }

        public IList<FeatureValue> GetValues(RawWordEncounter encounter, FeatureSelectionContext context)
        {
            return new[]
            {
                new FeatureValue(
                    context.ReorderedDictionary
                        .GetByName(encounter.Word)?.Meanings
                        .GetByName(encounter.Meaning)?.Id ?? 0)
            };
        }

        public IList<FeatureValue> GetNominalValues(FeatureSelectionContext context)
        {
            return Enumerable.Range(0, context.Project.DictionaryStatistics.MaxMeaningsPerWord + 1)
                .Select(x => new FeatureValue(x))
                .ToArray();
        }
    }
}
