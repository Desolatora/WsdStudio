using System.Globalization;
using System.Linq;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Readers.Input
{
    public class InputEmbeddingReader : BasicFileReader<RawEmbedding>
    {
        private string[] _occurrencesInData;
        private bool _skippedHeader;

        public InputEmbeddingReader(string path, string[] occurrencesInData) : base(path)
        {
            _occurrencesInData = occurrencesInData.Select(x => x.Replace("_", "").Replace("-", "")).ToArray();
        }

        public static EmbeddingDictionary ReadAll(
            string path, string[] occurrencesInData, IProgressHandle progress = null)
        {
            using (var reader = new InputEmbeddingReader(path, occurrencesInData))
            {
                return reader.ReadAll(progress).ToEmbeddingDictionary();
            }
        }

        protected override RawEmbedding Read()
        {
            if (!_skippedHeader)
            {
                SkipHeader();

                _skippedHeader = true;
            }

            if (BaseReader.EndOfStream)
                return null;

            var line = BaseReader.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
                return null;

            var splitLine = line.Trim(' ').Split(' ');

            if (!_occurrencesInData.Contains(splitLine[0].Replace("_", "").Replace("-", "")) &&
                splitLine[0] != RawWordEncounter.EndOfSentence)
                return null;

            return new RawEmbedding
            {
                Name = splitLine[0],
                Vector = splitLine
                    .Skip(1)
                    .Select(s => float.Parse(s, CultureInfo.InvariantCulture))
                    .ToArray()
            };
        }

        private void SkipHeader()
        {
            var line = BaseReader.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
                return;

            var splitLine = line.Trim(' ').Split(' ');

            if (splitLine.Length == 2)
                return;

            Restart();
        }
    }
}