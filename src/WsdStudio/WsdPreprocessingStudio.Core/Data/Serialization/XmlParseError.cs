using WsdPreprocessingStudio.Core.Helpers;

namespace WsdPreprocessingStudio.Core.Data.Serialization
{
    public class XmlParseError
    {
        public string EncounterId { get; set; }
        public TryGetMeaningStatus Error { get; set; }
    }
}
