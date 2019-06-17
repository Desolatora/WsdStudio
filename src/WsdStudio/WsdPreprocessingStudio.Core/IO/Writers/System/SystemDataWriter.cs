using System.Collections.Generic;
using System.IO;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Writers.System
{
    public class SystemDataWriter : BasicFileWriter<RawWordEncounter>
    {
        protected SystemDataWriter(string path) : base(path)
        {
        }

        public static void WriteAllFiles(
            string path, IList<(string FileName, RawWordEncounter[] Data)> files, 
            IProgressHandle progress = null)
        {
            var scope = progress?.Scope(files.Count);

            try
            {
                var counter = 0;

                foreach (var file in files)
                {
                    WriteAll(Path.Combine(path, file.FileName), file.Data);

                    scope?.TrySet(++counter);
                }
            }
            finally
            {
                scope?.Dispose();
            }
        }

        public static void WriteAll(
            string path, RawWordEncounter[] data, IProgressHandle progress = null)
        {
            using (var writer = new SystemDataWriter(path))
            {
                writer.WriteAll(data, progress);
            }
        }

        protected override void Write(RawWordEncounter data)
        {
            BaseWriter.WriteLine(string.Join(" ", data.Word, data.Pos, data.Meaning));
        }
    }
}