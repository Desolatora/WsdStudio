using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Extensions;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Readers.Input
{
    public class InputSynsetMappingReader : BasicFileReader<KeyValuePair<string, string>?>
    {
        public InputSynsetMappingReader(string path) : base(path)
        {
        }

        public static SynsetDictionary ReadAll(string path, IProgressHandle progress = null)
        {
            using (var reader = new InputSynsetMappingReader(path))
            {
                return new SynsetDictionary(
                    reader.ReadAll(progress)
                        .Select(x => x.GetValueOrDefault())
                        .DistinctBy(x => x.Key));
            }
        }

        protected override KeyValuePair<string, string>? Read()
        {
            if (BaseReader.EndOfStream)
                return null;

            var line = BaseReader.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
                return null;

            var split = line.Split(' ');

            if (split.Length != 2)
                return null;

            return new KeyValuePair<string, string>(split[0], split[1]);
        }
    }
}