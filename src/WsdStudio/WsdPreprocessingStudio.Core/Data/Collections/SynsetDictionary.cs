using System.Collections.Generic;

namespace WsdPreprocessingStudio.Core.Data.Collections
{
    public class SynsetDictionary : Dictionary<string, string>
    {
        public SynsetDictionary(IEnumerable<KeyValuePair<string, string>> items)
        {
            foreach (var item in items)
            {
                Add(item.Key, item.Value);
            }
        }
    }
}
