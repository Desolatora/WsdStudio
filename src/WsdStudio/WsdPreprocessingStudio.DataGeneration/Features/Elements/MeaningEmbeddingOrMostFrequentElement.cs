using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features.Elements
{
    public class MeaningEmbeddingOrMostFrequentElement : IFeatureElement
    {
        public string DisplayName => "MeaningEmbeddingOrMostFrequent";
        public string ArffAttributeName => "M_EMB_MF";
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

            var mostFrequentMeaning = context.ReorderedDictionary
                .GetByName(encounter.Word)?
                .Meanings.Values
                .SingleOrDefault(x => x.Id == 1 && x.PartOfSpeech == encounter.Pos)?
                .Meaning;
            
            var mostFrequentEmbedding = context.Project.MeaningEmbeddings
                .GetVectorOrDefault(mostFrequentMeaning);

            return FeatureValue.NewArray(mostFrequentEmbedding);
        }
    }
}
