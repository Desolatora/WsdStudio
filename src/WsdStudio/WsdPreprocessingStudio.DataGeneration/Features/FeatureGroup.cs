using System.Collections.Generic;
using WsdPreprocessingStudio.DataGeneration.Features.Elements;
using WsdPreprocessingStudio.DataGeneration.Features.Functions;
using WsdPreprocessingStudio.DataGeneration.Features.Sources;

namespace WsdPreprocessingStudio.DataGeneration.Features
{
    public class FeatureGroup
    {
        public IFeatureSource Source { get; set; }
        public FeatureValueType ValueType { get; set; }
        public IList<IFeatureElement> Elements { get; set; }
        public IAggregationFunction AggregationFunction { get; set; }
        public ICompressionFunction CompressionFunction { get; set; }
        public IList<IFeatureElement> CompressionElements { get; set; }
    }
}
