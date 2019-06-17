using System.Collections.Generic;

namespace WsdPreprocessingStudio.DataGeneration.Data
{
    public class DataSetByText
    {
        public DataSetName Name { get; }
        public IList<GeneratedTextData> Texts { get; }

        public DataSetByText(DataSetName name, IList<GeneratedTextData> texts)
        {
            Name = name;
            Texts = texts ?? new GeneratedTextData[0];
        }
    }
}
