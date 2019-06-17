using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.Data.Collections
{
    public class WordAnalysisDictionary : Dictionary<string, WordAnalysis>
    {
        public WordAnalysisDictionary()
        {
        }

        public WordAnalysisDictionary(IEnumerable<WordAnalysis> words)
        {
            foreach (var word in words)
                Add(word.Word, word);
        }

        public WordAnalysisDictionary Analyze(
            WordDictionary dictionary, TextData[] trainData, TextData[] testData,
            IProgressHandle progress = null)
        {
            var max = trainData.Length + testData.Length;
            var scope = progress?.Scope(max);
            var counter = 0;

            try
            {
                foreach (var text in trainData)
                {
                    foreach (var encounter in text.Data)
                    {
                        if (string.IsNullOrWhiteSpace(encounter.Word) ||
                            string.IsNullOrWhiteSpace(encounter.Meaning) ||
                            encounter.Word == RawWordEncounter.EmptyWord ||
                            encounter.Word == RawWordEncounter.EndOfSentence)
                            continue;

                        var wordAnalysis = GetOrAdd(dictionary, encounter);

                        wordAnalysis.TrainEncounters.AddEncounter(dictionary, encounter);
                        wordAnalysis.AllEncounters.AddEncounter(dictionary, encounter);
                    }

                    scope?.TrySet(++counter);
                }

                foreach (var text in testData)
                {
                    foreach (var encounter in text.Data)
                    {
                        if (string.IsNullOrWhiteSpace(encounter.Word) ||
                            string.IsNullOrWhiteSpace(encounter.Meaning) ||
                            encounter.Word == RawWordEncounter.EmptyWord ||
                            encounter.Word == RawWordEncounter.EndOfSentence)
                            continue;

                        var wordAnalysis = GetOrAdd(dictionary, encounter);

                        wordAnalysis.TestEncounters.AddEncounter(dictionary, encounter);
                        wordAnalysis.AllEncounters.AddEncounter(dictionary, encounter);
                    }

                    scope?.TrySet(++counter);
                }
            }
            finally
            {
                scope?.Dispose();
            }

            return this;
        }

        public WordAnalysis GetByName(string name)
        {
            return name == null || !ContainsKey(name)
                ? null
                : this[name];
        }
        
        private WordAnalysis GetOrAdd(WordDictionary dictionary, RawWordEncounter encounter)
        {
            var dictionaryWord = dictionary.GetByName(encounter.Word);
            var wordAnalysis = GetByName(encounter.Word);

            if (wordAnalysis == null)
            {
                wordAnalysis = new WordAnalysis
                {
                    Id = dictionaryWord?.Id ?? -1,
                    Word = encounter.Word
                };

                Add(encounter.Word, wordAnalysis);
            }

            return wordAnalysis;
        }

        public string[] GetAllWordOccurrences()
        {
            return this.Select(x => x.Value.Word).Distinct().ToArray();
        }

        public string[] GetAllMeaningOccurrences()
        {
            return this
                .SelectMany(x => x.Value.AllEncounters.Select(y => y.Key))
                .Distinct()
                .ToArray();
        }
    }

    public static class WsdWordAnalysisDictionaryExtensions
    {
        public static WordAnalysisDictionary ToWordAnalysisDictionary(
            this IEnumerable<WordAnalysis> words)
        {
            return new WordAnalysisDictionary(words);
        }
    }
}