using System;

namespace WsdPreprocessingStudio.DataGeneration.Resources
{
    internal static class MessageFormat
    {
        public static readonly Func<long, long, string> ReorderingDictionary =
            (c, m) => $"Reordering dictionary... ({c} of {m})";

        public static readonly Func<long, long, string> GeneratingRecords_Texts =
            (c, m) => $"Generating records... ({c} of {m} texts)";

        public static readonly Func<long, long, string> FormingGroups_DataSets =
            (c, m) => $"Forming groups... ({c} of {m} datasets)";

        public static readonly Func<long, long, string> ExtractingTestOnlySet_Groups =
            (c, m) => $"Extracting test only set... ({c} of {m} groups)";

        public static readonly Func<long, long, string> ExtractingValidationSet_Groups =
            (c, m) => $"Extracting validation set... ({c} of {m} groups)";

        public static readonly Func<long, long, string> ShufflingData_Groups =
            (c, m) => $"Shuffling data... ({c} of {m} groups)";

        public static readonly Func<long, long, string, string> SavingDataSet_Records =
            (c, m, d) => $"Saving dataset {d}... ({c} of {m} records)";

        public static readonly Func<long, long, string, long, long, string> SavingDataSet_RecordsAndDataSets =
            (c, m, d, dc, dm) => $"Saving dataset {d}... ({c} of {m} records, {dc} of {dm} datasets)";
    }
}
