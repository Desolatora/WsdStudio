using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core.Data.Collections;

namespace WsdPreprocessingStudio.Core.Helpers
{
    public static class SynsetHelper
    {
        public static string GetMeaning(
            WordDictionary dictionary, Dictionary<string, string> goldKeyDict, 
            Dictionary<string, string> synsetMapping, string word, string keyId)
        {
            if (string.IsNullOrWhiteSpace(keyId))
                return string.Empty;

            if (goldKeyDict.ContainsKey(keyId))
            {
                var synsetIdsRaw = goldKeyDict[keyId];

                if (string.IsNullOrWhiteSpace(synsetIdsRaw))
                    return string.Empty;

                var synsetIds = synsetIdsRaw.Split(' ');

                return synsetIds
                    .Where(x => !string.IsNullOrWhiteSpace(x) && synsetMapping.ContainsKey(x))
                    .Select(x => FromRawMeaning(synsetMapping[x]))
                    .OrderBy(x => dictionary.GetByName(word)?.Meanings.GetByName(x)?.Id ?? int.MaxValue)
                    .FirstOrDefault() ?? string.Empty;
            }

            return string.Empty;
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
}
