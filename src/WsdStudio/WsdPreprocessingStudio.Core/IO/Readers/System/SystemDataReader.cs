using System.Collections.Generic;
using System.IO;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Readers.System
{
    public class SystemDataReader : BasicFileReader<RawWordEncounter>
    {
        public SystemDataReader(string path) : base(path)
        {
        }

        public static TextData[] ReadAllFiles(
            string path, IList<(string fileName, string textName)> files,
            IProgressHandle progress = null)
        {
            var scope = progress?.Scope(files.Count);

            try
            {
                var result = new List<TextData>();
                var counter = 0;

                foreach (var (fileName, textName) in files)
                {
                    result.Add(new TextData(textName, ReadAll(Path.Combine(path, fileName))));

                    scope?.TrySet(++counter);
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
            using (var reader = new SystemDataReader(path))
            {
                return reader.ReadAll(progress);
            }
        }

        protected override RawWordEncounter Read()
        {
            if (BaseReader.EndOfStream)
                return null;

            var line = BaseReader.ReadLine();
            var columns = line.Split(' ');

            return new RawWordEncounter
            {
                Word = columns[0],
                Pos = columns[1],
                Meaning = columns[2]
            };
        }
    }
}