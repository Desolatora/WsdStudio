using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Elements
{
    public class WordEmbeddingElement : IFeatureElement
    {
        public string DisplayName => "WordEmbedding";
        public string ArffAttributeName => "W_EMB";
        public FeatureValueType FeatureType => FeatureValueType.Numeric;

        public int GetValueCount(FeatureSelectionContext context)
        {
            return context.Project.WordEmbeddings.VectorLength;
        }

        public IList<FeatureValue> GetValues(RawWordEncounter encounter, FeatureSelectionContext context)
        {
            var embedding = context.Project.WordEmbeddings.GetVectorOrDefault(encounter.Word);

            return FeatureValue.NewArray(embedding);
        }
    }
}
