using System.Globalization;
using System.Linq;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Writers.System
{
    public class SystemEmbeddingWriter : BasicFileWriter<RawEmbedding>
    {
        private const string FloatFormat = "0.#######";

        protected SystemEmbeddingWriter(string path) : base(path)
        {
        }

        public static void WriteAll(
            string path, EmbeddingDictionary embeddings, IProgressHandle progress = null)
        {
            using (var writer = new SystemEmbeddingWriter(path))
            {
                writer.WriteAll(embeddings.Values.ToArray(), progress);
            }
        }

        protected override void Write(RawEmbedding data)
        {
            var line = string.Join(" ",
                data.Name,
                string.Join(" ", data.Vector
                    .Select(x => x.ToString(FloatFormat, CultureInfo.InvariantCulture))));

            BaseWriter.WriteLine(line);
        }
    }
}