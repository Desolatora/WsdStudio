using System;
using System.Collections.Generic;
using System.Linq;

namespace WsdPreprocessingStudio.DataGeneration.Data
{
    public class GenerationInfoReadable
    {
        public string GeneratedByAppVersion { get; set; }
        public int LeftContext { get; set; }
        public int RightContext { get; set; }
        public bool Overlap { get; set; }
        public bool ExtractValidationSet { get; set; }
        public int ValidationSetPercentage { get; set; }
        public bool ShuffleData { get; set; }
        public string SavingStrategy { get; set; }
        public string OutputFormat { get; set; }
        public string OrderMeanings { get; set; }
        public string OrderMeaningsStrategy { get; set; }
        public IList<string> FilteredPosList { get; set; }
        public IList<FeatureGroupReadable> FeatureGroups { get; set; }

        public GenerationInfoReadable(GenerationInfo info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            GeneratedByAppVersion =
                typeof(GenerationInfoReadable).Assembly.GetName().Version.ToString();
            LeftContext = info.LeftContext;
            RightContext = info.RightContext;
            Overlap = info.Overlap;
            ExtractValidationSet = info.ExtractValidationSet;
            ValidationSetPercentage = info.ValidationSetPercentage;
            ShuffleData = info.ShuffleData;
            SavingStrategy = info.SavingStrategy.ToString();
            OutputFormat = info.OutputFormat.ToString();
            OrderMeanings = info.OrderMeanings.ToString();
            OrderMeaningsStrategy = info.OrderMeaningsStrategy.ToString();
            FilteredPosList = info.FilteredPosList;
            FeatureGroups = info.FeatureGroups
                .Select(x => new FeatureGroupReadable(x))
                .ToArray();
        }
    }
}
