using System.Collections.Generic;

namespace WsdPreprocessingStudio.Core.Data.Collections
{
    public class EmbeddingDictionary : Dictionary<string, RawEmbedding>
    {
        public int VectorLength { get; } = -1;

        public EmbeddingDictionary(IEnumerable<RawEmbedding> words)
        {
            foreach (var word in words)
            {
                if (VectorLength < 0)
                    VectorLength = word.Vector.Length;

                if (!ContainsKey(word.Name))
                    Add(word.Name, word);
            }
        }

        public float[] GetVectorOrDefault(string name)
        {
            if (name == null)
                return new float[VectorLength];

            if (ContainsKey(name))
                return this[name].Vector;

            var newName = name.Replace("_", "-");

            if (ContainsKey(newName))
                return this[newName].Vector;

            newName = name.Replace("_", "");

            if (ContainsKey(newName))
                return this[newName].Vector;

            return new float[VectorLength];
        }
    }

    public static class WsdEmbeddingDictionaryExtensions
    {
        public static EmbeddingDictionary ToEmbeddingDictionary(
            this IEnumerable<RawEmbedding> words)
        {
            return new EmbeddingDictionary(words);
        }
    }
}