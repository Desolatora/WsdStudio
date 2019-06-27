using System;

namespace WsdPreprocessingStudio.DataGeneration.Resources
{
    internal static class MessageFormat
    {
        public static readonly Func<long, long, string> GeneratingData =
            (c, m) => $"Generating data... ({c} of {m})";
        
        public static readonly Func<long, long, string, string> SavingDataSet_Records =
            (c, m, d) => $"Saving dataset {d}... ({c} of {m} records)";

        public static readonly Func<long, long, string, long, long, string> SavingDataSet_RecordsAndDataSets =
            (c, m, d, dc, dm) => $"Saving dataset {d}... ({c} of {m} records, {dc} of {dm} datasets)";
    }
}
