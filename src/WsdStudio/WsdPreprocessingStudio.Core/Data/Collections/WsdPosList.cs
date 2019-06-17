using System.Collections.Generic;
using System.Linq;

namespace WsdPreprocessingStudio.Core.Data.Collections
{
    public class WsdPosList : List<string>
    {
        public const string EmptyPos = "EMPTY";
        public float[] EmptyVector => GetVector(string.Empty);

        public WsdPosList(TextData[] data)
        {
            var posList = data
                .SelectMany(x => x.Data)
                .GroupBy(x => x.Pos)
                .Select(x => x.Key)
                .Where(x => !string.IsNullOrWhiteSpace(x));

            AddRange(posList);
            Sort();
        }

        public WsdPosList(IEnumerable<string> list) : base(list)
        {
            Sort();
        }

        public float[] GetVector(string pos)
        {
            var result = new float[Count];
            var index = IndexOf(pos);

            if (index > -1)
                result[index] = 1f;

            return result;
        }

        public string GetOrDefault(string pos)
        {
            return Contains(pos)
                ? pos
                : EmptyPos;
        }
    }
}
