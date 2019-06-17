using System;
using WsdPreprocessingStudio.Core.Helpers;

namespace WsdPreprocessingStudio.Core.Resources
{
    internal static class MessageFormat
    {
        public static readonly Func<long, long, string> LoadingPlugins_Files =
            (c, m) => $"Loading plugins... ({c} of {m} files)";

        public static readonly Func<long, long, string> LoadingDictionary_Bytes =
            (c, m) => $"Loading dictionary... ({StringEx.ToSize(c)} of {StringEx.ToSize(m)})";

        public static readonly Func<long, long, string> LoadingWordEmbeddings_Bytes =
            (c, m) => $"Loading word embeddings... ({StringEx.ToSize(c)} of {StringEx.ToSize(m)})";

        public static readonly Func<long, long, string> LoadingMeaningEmbeddings_Bytes =
            (c, m) => $"Loading meaning embeddings... ({StringEx.ToSize(c)} of {StringEx.ToSize(m)})";
        
        public static readonly Func<long, long, string> LoadingSynsetMappings_Bytes =
            (c, m) => $"Loading synset mappings... ({StringEx.ToSize(c)} of {StringEx.ToSize(m)})";

        public static readonly Func<long, long, string> LoadingDataAnalysis_Bytes =
            (c, m) => $"Loading data analysis... ({StringEx.ToSize(c)} of {StringEx.ToSize(m)})";

        public static readonly Func<long, long, string> LoadingTrainData_Files =
            (c, m) => $"Loading train data... ({c} of {m} files)";

        public static readonly Func<long, long, string> LoadingTestData_Files =
            (c, m) => $"Loading test data... ({c} of {m} files)";
        
        public static readonly Func<long, long, string> AnalyzingData_Files =
            (c, m) => $"Analyzing data... ({c} of {m} files)";

        public static readonly Func<long, long, string> ComputingDictionaryStatistics =
            (c, m) => $"Computing dictionary statistics... ({c} of {m})";

        public static readonly Func<long, long, string> ComputingDataStatistics =
            (c, m) => $"Computing data statistics... ({c} of {m})";

        public static readonly Func<long, long, string> SavingDictionary_Words =
            (c, m) => $"Saving dictionary... ({c} of {m} words)";

        public static readonly Func<long, long, string> SavingTrainData_Files =
            (c, m) => $"Saving train data... ({c} of {m} files)";

        public static readonly Func<long, long, string> SavingTestData_Files =
            (c, m) => $"Saving test data... ({c} of {m} files)";

        public static readonly Func<long, long, string> SavingWordEmbeddings_Embeddings =
            (c, m) => $"Saving word embeddings... ({c} of {m} embeddings)";

        public static readonly Func<long, long, string> SavingMeaningEmbeddings_Embeddings =
            (c, m) => $"Saving meaning embeddings... ({c} of {m} embeddings)";

        public static readonly Func<long, long, string> SavingDataAnalysis_Words =
            (c, m) => $"Saving data analysis... ({c} of {m} words)";
    }
}
