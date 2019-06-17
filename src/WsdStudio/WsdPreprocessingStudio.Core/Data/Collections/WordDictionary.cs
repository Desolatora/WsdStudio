using System.Collections.Generic;

namespace WsdPreprocessingStudio.Core.Data.Collections
{
    public class WordDictionary : Dictionary<string, DictionaryWord>
    {
        public WordDictionary(IEnumerable<DictionaryWord> words)
        {
            foreach (var word in words)
            {
                Add(word.Word, word);
            }
        }
        
        public DictionaryWord GetByName(string name)
        {
            return name == null || !ContainsKey(name)
                ? null
                : this[name];
        }
    }

    public static class WsdWordDictionaryExtensions
    {
        public static WordDictionary ToWordDictionary(
            this IEnumerable<DictionaryWord> words)
        {
            return new WordDictionary(words);
        }
    }
}