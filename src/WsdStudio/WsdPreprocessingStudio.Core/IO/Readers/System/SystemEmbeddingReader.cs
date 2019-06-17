using System.Globalization;
using System.Linq;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Readers.System
{
    public class SystemEmbeddingReader : BasicFileReader<RawEmbedding>
    {
        public SystemEmbeddingReader(string path) : base(path)
        {
        }

        public static EmbeddingDictionary ReadAll(string path, IProgressHandle progress = null)
        {
            using (var reader = new SystemEmbeddingReader(path))
            {
                return reader.ReadAll(progress).ToEmbeddingDictionary();
            }
        }

        protected override RawEmbedding Read()
        {
            if (BaseReader.EndOfStream)
                return null;

            var line = BaseReader.ReadLine();
            var columns = line.Split(' ');

            return new RawEmbedding
            {
                Name = columns[0],
                Vector = columns
                    .Skip(1)
                    .Select(s => float.Parse(s, CultureInfo.InvariantCulture))
                    .ToArray()
            };
        }
    }
}