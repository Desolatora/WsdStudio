using WsdPreprocessingStudio.Core.Data.Serialization;
using WsdPreprocessingStudio.Core.Helpers;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Writers.Misc
{
    public class XmlParseErrorWriter : BasicFileWriter<XmlParseError>
    {
        protected XmlParseErrorWriter(string path) : base(path)
        {
        }

        public static void WriteAll(
            string path, XmlParseError[] errors, IProgressHandle progress = null)
        {
            using (var writer = new XmlParseErrorWriter(path))
            {
                writer.WriteAll(errors, progress);
            }
        }

        protected override void Write(XmlParseError data)
        {
            switch (data.Error)
            {
                case TryGetMeaningStatus.OK:
                    break;
                case TryGetMeaningStatus.IdNotPresentInGoldKeyDictionary:
                    BaseWriter.WriteLine(data.EncounterId + " - Id is not present in gold key file.");
                    break;
                case TryGetMeaningStatus.NoSynsetMappingFound:
                    BaseWriter.WriteLine(data.EncounterId + " - No synset mapping found.");
                    break;
            }
        }
    }
}