using System.Linq;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Writers.System
{
    public class SystemDataAnalysisWriter : BasicFileWriter<WordAnalysis>
    {
        protected SystemDataAnalysisWriter(string path) : base(path)
        {
        }

        public static void WriteAll(
            string path, WordAnalysisDictionary dictionary, IProgressHandle progress = null)
        {
            using (var writer = new SystemDataAnalysisWriter(path))
            {
                writer.WriteAll(dictionary.Values.ToArray(), progress);
            }
        }

        protected override void Write(WordAnalysis data)
        {
            var line = string.Join(" ",
                data.Id, data.Word,
                string.Join("|", data.TrainEncounters.Values
                    .Select(x => string.Join(":", x.Id, x.Meaning, x.Encounters))),
                string.Join("|", data.TestEncounters.Values
                    .Select(x => string.Join(":", x.Id, x.Meaning, x.Encounters))),
                string.Join("|", data.AllEncounters.Values
                    .Select(x => string.Join(":", x.Id, x.Meaning, x.Encounters))));

            BaseWriter.WriteLine(line);
        }
    }
}