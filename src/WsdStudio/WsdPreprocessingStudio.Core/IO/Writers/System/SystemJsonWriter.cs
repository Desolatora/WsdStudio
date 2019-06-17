using System;
using System.IO;
using Newtonsoft.Json;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Writers.System
{
    public class SystemJsonWriter
    {
        public static void Write<T>(
            string path, T data, IProgressHandle progress = null, bool includeTypeNames = true)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var scope = progress?.Scope(1);

            try
            {
                var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = includeTypeNames
                        ? TypeNameHandling.Objects
                        : TypeNameHandling.None
                });

                File.WriteAllText(path, json);
            }
            finally
            {
                scope?.Dispose();
            }
        }
    }
}