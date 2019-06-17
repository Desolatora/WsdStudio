using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Elements
{
    public class MeaningEmbeddingElement : IFeatureElement
    {
        public string DisplayName => "MeaningEmbedding";
        public string ArffAttributeName => "M_EMB";
        public FeatureValueType FeatureType => FeatureValueType.Numeric;

        public int GetValueCount(FeatureSelectionContext context)
        {
            return context.Project.MeaningEmbeddings.VectorLength;
        }

        public IList<FeatureValue> GetValues(RawWordEncounter encounter, FeatureSelectionContext context)
        {
            var embedding = context.Project.MeaningEmbeddings.GetVectorOrDefault(encounter.Meaning);

            return FeatureValue.NewArray(embedding);
        }
    }
}
