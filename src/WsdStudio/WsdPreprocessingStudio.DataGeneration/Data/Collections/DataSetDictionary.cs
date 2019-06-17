using System.Collections.Generic;

namespace WsdPreprocessingStudio.DataGeneration.Data.Collections
{
    public class DataSetDictionary : Dictionary<DataSetName, DataSet>
    {
        public DataSet GetByName(DataSetName name)
        {
            return !ContainsKey(name)
                ? null
                : this[name];
        }
    }
}
