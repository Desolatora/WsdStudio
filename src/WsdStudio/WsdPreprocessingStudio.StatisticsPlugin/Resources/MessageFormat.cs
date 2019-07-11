using System;

namespace WsdPreprocessingStudio.StatisticsPlugin.Resources
{
    internal static class MessageFormat
    {
        public static readonly Func<long, long, string> ComputingStatistics_Groups =
            (c, m) => $"Computing statistics... ({c} of {m} groups)";
    }
}
