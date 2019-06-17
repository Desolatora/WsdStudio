using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Helpers;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Readers.Input
{
    public class InputPlainTextDataReader : BasicFileReader<RawWordEncounter>
    {
        public Regex ParseRegex { get; set; }

        public InputPlainTextDataReader(string path) : base(path)
        {
            ParseRegex = new Regex(@"^(?<word>[^\s:]+?)[\s:]+(?<meaning>[^\s:]+)(:\d+)?$");
        }

        public static TextData[] ReadAllFiles(string path, IProgressHandle progress = null)
        {
            var dataFiles = Directory.GetFiles(path);
            var scope = progress?.Scope(dataFiles.Length);

            try
            {
                var result = new List<TextData>();

                for (var i = 0; i < dataFiles.Length; i++)
                {
                    var file = dataFiles[i];
                    var textName = Path.GetFileNameWithoutExtension(file);

                    result.Add(new TextData(textName, ReadAll(file)));

                    scope.TrySet(i + 1);
                }

                return result.ToArray();
            }
            finally
            {
                scope?.Dispose();
            }
        }

        public static RawWordEncounter[] ReadAll(string path, IProgressHandle progress = null)
        {
            using (var reader = new InputPlainTextDataReader(path))
            {
                return reader.ReadAll(progress);
            }
        }

        protected override RawWordEncounter Read()
        {
            if (BaseReader.EndOfStream)
                return null;

            var line = BaseReader.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
                return new RawWordEncounter
                {
                    Word = RawWordEncounter.EndOfSentence
                };

            var match = ParseRegex.Match(line);

            if (!match.Success)
                return new RawWordEncounter
                {
                    Word = RawWordEncounter.EndOfSentence
                };

            return new RawWordEncounter
            {
                Word = match.Groups["word"].Value,
                Pos = SynsetHelper.GetPos(match.Groups["meaning"].Value),
                Meaning = match.Groups["meaning"].Value
            };
        }
    }
}