using System;
using System.Collections.Generic;
using System.IO;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Readers
{
    public abstract class BasicFileReader<T> : IDisposable
    {
        public StreamReader BaseReader { get; private set; }
        
        protected BasicFileReader(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(path);

            BaseReader = new StreamReader(
                new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }
        
        public T[] ReadAll(IProgressHandle progress = null)
        {
            var scope = progress?.Scope(BaseReader.BaseStream.Length);

            try
            {
                var list = new List<T>();

                while (!BaseReader.EndOfStream)
                {
                    var data = Read();

                    if (data != null)
                        list.Add(data);

                    scope?.TrySet(BaseReader.BaseStream.Position);
                }

                return list.ToArray();
            }
            finally
            {
                scope?.Dispose();
            }
        }
        
        public void Restart()
        {
            BaseReader.DiscardBufferedData();
            BaseReader.BaseStream.Position = 0;
        }

        protected abstract T Read();
        
        public void Dispose()
        {
            BaseReader?.Dispose();
            BaseReader = null;
        }
    }
}
