using System.Linq;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Extensions;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.Data.Statistics
{
    public class DictionaryStatistics
    {
        public int WordCount { get; set; }
        public int MonosemanticWordCount { get; set; }
        public int PolysemanticWordCount { get; set; }
        public int MaxMeaningsPerWord { get; set; }
        public double AverageMeaningsPerWord { get; set; }
        public int UniqueMeaningsCount { get; set; }

        public DictionaryStatistics Compute(WordDictionary dictionary, IProgressHandle progress = null)
        {
            var scope = progress?.Scope(1);

            try
            {
                WordCount = dictionary.Count;
                MonosemanticWordCount = dictionary.Values.Count(x => x.Meanings.Count == 1);
                PolysemanticWordCount = dictionary.Values.Count(x => x.Meanings.Count > 1);
                MaxMeaningsPerWord = dictionary.Values.MaxOrDefault(x => x.Meanings.Count);
                AverageMeaningsPerWord = dictionary.Values.AverageOrDefault(x => x.Meanings.Count);
                UniqueMeaningsCount = dictionary.Values.SelectMany(x => x.Meanings.Keys).Distinct().Count();
            }
            finally
            {
                scope?.Dispose();
            }

            return this;
        }
    }
}
