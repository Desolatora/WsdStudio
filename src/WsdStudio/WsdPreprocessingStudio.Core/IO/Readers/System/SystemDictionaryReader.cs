using System.Linq;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Helpers;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Readers.System
{
    public class SystemDictionaryReader : BasicFileReader<DictionaryWord>
    {
        public SystemDictionaryReader(string path) : base(path)
        {
        }

        public static WordDictionary ReadAll(string path, IProgressHandle progress = null)
        {
            using (var reader = new SystemDictionaryReader(path))
            {
                return reader.ReadAll(progress).ToWordDictionary();
            }
        }

        protected override DictionaryWord Read()
        {
            if (BaseReader.EndOfStream)
                return null;

            var line = BaseReader.ReadLine();
            var columns = line.Split(' ');

            return new DictionaryWord
            {
                Id = int.Parse(columns[0]),
                Word = columns[1],
                Meanings = columns
                    .Skip(2)
                    .Select(x =>
                    {
                        var values = x.Split(':');

                        return new DictionaryMeaning
                        {
                            Id = int.Parse(values[0]),
                            Meaning = values[1],
                            PartOfSpeech = SynsetHelper.GetPos(values[1]),
                            Encounters = int.Parse(values[2])
                        };
                    })
                    .ToMeaningDictionary()
            };
        }
    }
}