using System;
using System.Collections.Generic;
using System.IO;
using WsdPreprocessingStudio.Core.Helpers;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Writers
{
    public abstract class BasicFileWriter<T> : IDisposable
    {
        public StreamWriter BaseWriter { get; private set; }

        protected BasicFileWriter(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            DirectoryEx.CreateFor(path);

            BaseWriter = new StreamWriter(
                new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite));
        }

        public void WriteAll(IList<T> list, IProgressHandle progress = null)
        {
            var scope = progress?.Scope(list.Count);

            try
            {
                for (var i = 0; i < list.Count; i++)
                {
                    Write(list[i]);

                    scope?.TrySet(i + 1);
                }
            }
            finally
            {
                scope?.Dispose();
            }
        }
        
        protected abstract void Write(T data);

        public void Dispose()
        {
            BaseWriter?.Dispose();
            BaseWriter = null;
        }
    }
}