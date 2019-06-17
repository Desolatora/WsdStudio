using System.Linq;
using System.Text.RegularExpressions;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Helpers;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Readers.Input
{
    public class InputDictionaryReader : BasicFileReader<DictionaryWord>
    {
        public Regex ParseRegex { get; set; }

        private int _wordCounter;

        public InputDictionaryReader(string path) : base(path)
        {
            ParseRegex = new Regex(@"^(?<word>\S+?)(\s+(?<meaning>[^\s:]+(:\d+)?))*$");
        }

        public static WordDictionary ReadAll(string path, IProgressHandle progress = null)
        {
            using (var reader = new InputDictionaryReader(path))
            {
                return reader.ReadAll(progress).ToWordDictionary();
            }
        }

        protected override DictionaryWord Read()
        {
            if (BaseReader.EndOfStream)
                return null;

            var line = BaseReader.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
                return null;

            var match = ParseRegex.Match(line);

            if (!match.Success)
                return null;

            return new DictionaryWord
            {
                Id = ++_wordCounter,
                Word = match.Groups["word"].Value,
                Meanings = match.Groups["meaning"].Captures
                    .Cast<Capture>()
                    .Select((x, i) =>
                    {
                        var split = x.Value.Split(':');

                        if (split.Length == 1)
                            return new DictionaryMeaning
                            {
                                Id = i + 1,
                                Meaning = split[0],
                                PartOfSpeech = SynsetHelper.GetPos(split[0])
                            };

                        int.TryParse(split[1], out var encounters);

                        return new DictionaryMeaning
                        {
                            Id = i + 1,
                            Meaning = split[0],
                            PartOfSpeech = SynsetHelper.GetPos(split[0]),
                            Encounters = encounters
                        };
                    })
                    .ToMeaningDictionary()
            };
        }
    }
}