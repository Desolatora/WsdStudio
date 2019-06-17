using System;
using System.Collections.Generic;

namespace WsdPreprocessingStudio.DataGeneration.Data
{
    public class GeneratedTextData
    {
        public string TextName { get; }
        public IList<RawRecord> Data { get; }

        public GeneratedTextData(string textName, IList<RawRecord> data)
        {
            TextName = textName ?? throw new ArgumentNullException(nameof(textName));
            Data = data ?? new RawRecord[0];
        }
    }
}
