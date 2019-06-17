using WsdPreprocessingStudio.Core.Data.Collections;

namespace WsdPreprocessingStudio.Core.Data.Statistics
{
    public class EmbeddingStatistics
    {
        public int EmbeddingCount { get; set; }
        public int VectorLength { get; set; }

        public EmbeddingStatistics Compute(EmbeddingDictionary dictionary)
        {
            EmbeddingCount = dictionary.Count;
            VectorLength = dictionary.VectorLength;

            return this;
        }
    }
}
