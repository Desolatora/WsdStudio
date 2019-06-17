using System.Collections.Generic;

namespace WsdPreprocessingStudio.DataGeneration.Data
{
    public class DataSet
    {
        public DataSetName Name { get; }
        public IList<RawRecord> Data { get; }

        public DataSet(DataSetName name, IList<RawRecord> data)
        {
            Name = name;
            Data = data ?? new RawRecord[0];
        }
    }

    public enum DataSetName
    {
        Train,
        Test,
        Validation,
        TestOnly
    }
}
