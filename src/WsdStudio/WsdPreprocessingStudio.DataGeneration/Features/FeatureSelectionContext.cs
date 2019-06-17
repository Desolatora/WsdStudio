using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Features
{
    public class FeatureSelectionContext
    {
        public GenerationInfo GenerationInfo { get; set; }
        public WordDictionary ReorderedDictionary { get; set; }
        public WsdPosList FilteredPosList { get; set; }
        public WsdProject Project { get; set; }
        public DataSetName DataSetName { get; set; }
    }
}
