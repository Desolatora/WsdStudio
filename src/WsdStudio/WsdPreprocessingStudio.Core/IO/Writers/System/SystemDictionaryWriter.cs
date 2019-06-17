using System.Linq;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Writers.System
{
    public class SystemDictionaryWriter : BasicFileWriter<DictionaryWord>
    {
        protected SystemDictionaryWriter(string path) : base(path)
        {
        }

        public static void WriteAll(
            string path, WordDictionary dictionary, IProgressHandle progress = null)
        {
            using (var writer = new SystemDictionaryWriter(path))
            {
                writer.WriteAll(dictionary.Values.ToArray(), progress);
            }
        }

        protected override void Write(DictionaryWord data)
        {
            var line = string.Join(" ",
                data.Id, data.Word,
                string.Join(" ", data.Meanings.Values
                    .Select(x => string.Join(":", x.Id, x.Meaning, x.Encounters))));

            BaseWriter.WriteLine(line);
        }
    }
}