using System;
using System.Linq;
using WsdPreprocessingStudio.DataGeneration.Features;

namespace WsdPreprocessingStudio.DataGeneration.Data
{
    public class FeatureGroupReadable
    {
        public string Source { get; set; }
        public string Type { get; set; }
        public string Elements { get; set; }
        public string AggregationFunction { get; set; }
        public string CompressionFunction { get; set; }
        public string CompressionElements { get; set; }

        public FeatureGroupReadable(FeatureGroup group)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            Source = group.Source?.DisplayName;
            Type = group.ValueType.ToString();
            Elements = group.Elements != null
                ? string.Join(", ", group.Elements.Select(x => x.DisplayName))
                : null;
            AggregationFunction = group.AggregationFunction?.DisplayName;
            CompressionFunction = group.CompressionFunction?.DisplayName;
            CompressionElements = group.CompressionElements != null
                ? string.Join(", ", group.CompressionElements.Select(x => x.DisplayName))
                : null;
        }
    }
}