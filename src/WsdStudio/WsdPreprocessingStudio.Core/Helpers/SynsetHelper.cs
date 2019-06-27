using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core.Data.Collections;

namespace WsdPreprocessingStudio.Core.Helpers
{
    public static class SynsetHelper
    {
        public static TryGetMeaningStatus TryGetMeaning(
            WordDictionary dictionary, Dictionary<string, string> goldKeyDictionary, 
            SynsetDictionary synsetMappings, string word, string keyId, out string meaning)
        {
            meaning = string.Empty;

            if (goldKeyDictionary.ContainsKey(keyId))
            {
                var senseKeyList = goldKeyDictionary[keyId];
                var senseKeys = senseKeyList.Split(' ');
                var mostFrequentMeaning = senseKeys
                    .Where(synsetMappings.ContainsKey)
                    .Select(x => FromRawMeaning(synsetMappings[x]))
                    .OrderBy(x => dictionary.GetByName(word)?.Meanings.GetByName(x)?.Id ?? int.MaxValue)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(mostFrequentMeaning))
                    return TryGetMeaningStatus.NoSynsetMappingFound;
                
                meaning = mostFrequentMeaning;

                return TryGetMeaningStatus.OK;
            }

            return TryGetMeaningStatus.IdNotPresentInGoldKeyDictionary;
        }

        public static string GetPos(string meaning)
        {
            if (string.IsNullOrWhiteSpace(meaning))
                return string.Empty;

            switch (meaning[9])
            {
                case 'n':
                    return "NOUN";
                case 'v':
                    return "VERB";
                case 'a':
                    return "ADJ";
                case 'r':
                    return "ADV";
                default:
                    return string.Empty;
            }
        }

        private static string FromRawMeaning(string rawMeaning)
        {
            if (string.IsNullOrWhiteSpace(rawMeaning))
                return string.Empty;

            var pos = string.Empty;

            switch (rawMeaning[0])
            {
                case '1':
                    pos = "n";
                    break;
                case '2':
                    pos = "v";
                    break;
                case '3':
                    pos = "a";
                    break;
                case '4':
                    pos = "r";
                    break;
            }

            return rawMeaning.Substring(1) + "-" + pos;
        }
    }

    public enum TryGetMeaningStatus
    {
        OK,
        IdNotPresentInGoldKeyDictionary,
        NoSynsetMappingFound
    }
}
