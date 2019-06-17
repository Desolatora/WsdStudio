using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Elements
{
    public class MeaningEmbeddingOrAverageElement : IFeatureElement
    {
        public string DisplayName => "MeaningEmbeddingOrAverage";
        public string ArffAttributeName => "M_EMB_AVG";
        public FeatureValueType FeatureType => FeatureValueType.Numeric;

        public int GetValueCount(FeatureSelectionContext context)
        {
            return context.Project.MeaningEmbeddings.VectorLength;
        }

        public IList<FeatureValue> GetValues(RawWordEncounter encounter, FeatureSelectionContext context)
        {
            if (context.DataSetName == DataSetName.Train ||
                context.DataSetName == DataSetName.Validation)
            {
                var embedding = context.Project.MeaningEmbeddings.GetVectorOrDefault(encounter.Meaning);

                return FeatureValue.NewArray(embedding);
            }

            var meanings = context.ReorderedDictionary
                .GetByName(encounter.Word)?
                .Meanings.Values
                .Where(x => x.PartOfSpeech == encounter.Pos)
                .ToArray();

            if (meanings == null || meanings.Length == 0)
                return FeatureValue.NewArray(0, context.Project.MeaningEmbeddings.VectorLength);

            var averageEmbedding = new float[context.Project.MeaningEmbeddings.VectorLength];
            
            for (var m = 0; m < meanings.Length; m++)
            {
                var embedding = context.Project.MeaningEmbeddings.GetVectorOrDefault(meanings[m].Meaning);

                for (var i = 0; i < averageEmbedding.Length; i++)
                {
                    averageEmbedding[i] += embedding[i];
                }
            }

            for (var i = 0; i < averageEmbedding.Length; i++)
            {
                averageEmbedding[i] /= meanings.Length;
            }

            return FeatureValue.NewArray(averageEmbedding);
        }
    }
}
