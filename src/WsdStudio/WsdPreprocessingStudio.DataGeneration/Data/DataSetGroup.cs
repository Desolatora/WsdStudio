using WsdPreprocessingStudio.DataGeneration.Data.Collections;

namespace WsdPreprocessingStudio.DataGeneration.Data
{
    public class DataSetGroup
    {
        public string GroupName { get; }
        public DataSetDictionary DataSets { get; }

        public DataSetGroup() : this(null)
        {
        }

        public DataSetGroup(string groupName, DataSetDictionary dataSets = null)
        {
            GroupName = groupName ?? string.Empty;
            DataSets = dataSets ?? new DataSetDictionary();
        }
    }
}
