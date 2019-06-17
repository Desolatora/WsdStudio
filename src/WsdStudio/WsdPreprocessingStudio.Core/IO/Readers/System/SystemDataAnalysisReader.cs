using System.Linq;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Helpers;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Readers.System
{
    public class SystemDataAnalysisReader : BasicFileReader<WordAnalysis>
    {
        public SystemDataAnalysisReader(string path) : base(path)
        {
        }

        public static WordAnalysisDictionary ReadAll(string path, IProgressHandle progress = null)
        {
            using (var reader = new SystemDataAnalysisReader(path))
            {
                return reader.ReadAll(progress).ToWordAnalysisDictionary();
            }
        }

        protected override WordAnalysis Read()
        {
            if (BaseReader.EndOfStream)
                return null;

            var line = BaseReader.ReadLine();
            var columns = line.Split(' ');

            return new WordAnalysis
            {
                Id = int.Parse(columns[0]),
                Word = columns[1],
                TrainEncounters = ParseEncounters(columns[2]),
                TestEncounters = ParseEncounters(columns[3]),
                AllEncounters = ParseEncounters(columns[4])
            };
        }

        private MeaningDictionary ParseEncounters(string value)
        {
            return value
                .Split('|')
                .Where(x => !string.IsNullOrEmpty(x))
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
                .ToMeaningDictionary();
        }
    }
}