using System;
using System.IO;
using Newtonsoft.Json;
using WsdPreprocessingStudio.Core.Resources;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.IO.Readers.System
{
    public class SystemJsonReader
    {
        public static T Read<T>(string path, IProgressHandle progress = null)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var scope = progress?.Scope(1);

            try
            {
                var data = File.ReadAllText(path);
                
                try
                {
                    return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        TypeNameHandling = TypeNameHandling.Objects
                    });
                }
                catch (Exception ex)
                {
                    throw new Exception(ExceptionMessage.UnableToLoadProjectData, ex);
                }
            }
            finally
            {
                scope?.Dispose();
            }
        }
    }
}