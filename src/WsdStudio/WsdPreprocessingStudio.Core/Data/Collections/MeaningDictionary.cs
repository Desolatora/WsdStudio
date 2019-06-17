using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Helpers;

namespace WsdPreprocessingStudio.Core.Data.Collections
{
    public class MeaningDictionary : Dictionary<string, DictionaryMeaning>
    {
        public MeaningDictionary()
        {
        }

        public MeaningDictionary(IEnumerable<DictionaryMeaning> meanings)
        {
            foreach (var meaning in meanings)
                Add(meaning.Meaning, meaning);
        }
        
        public DictionaryMeaning GetByName(string name)
        {
            return name == null || !ContainsKey(name)
                ? null
                : this[name];
        }

        internal void AddEncounter(WordDictionary dictionary, RawWordEncounter encounter)
        {
            var dictionaryWord = dictionary.GetByName(encounter.Word);
            var dictionaryMeaning = dictionaryWord?.Meanings.GetByName(encounter.Meaning);

            var meaningAnalysis = GetByName(encounter.Meaning);

            if (meaningAnalysis == null)
            {
                meaningAnalysis = new DictionaryMeaning
                {
                    Id = dictionaryMeaning?.Id ?? -1,
                    Meaning = encounter.Meaning,
                    PartOfSpeech = SynsetHelper.GetPos(encounter.Meaning)
                };

                Add(encounter.Meaning, meaningAnalysis);
            }

            meaningAnalysis.Encounters++;
        }
    }

    public static class WsdMeaningDictionaryExtensions
    {
        public static MeaningDictionary ToMeaningDictionary(
            this IEnumerable<DictionaryMeaning> meanings)
        {
            return new MeaningDictionary(meanings);
        }
    }
}