using System.IO;
using System.Text.RegularExpressions;

namespace WsdPreprocessingStudio.Core.Helpers
{
    public static class PathEx
    {
        public static string CleanFileName(string fileName)
        {
            var invalidChars = new string(Path.GetInvalidFileNameChars());

            return Regex.Replace(fileName, $"[{Regex.Escape(invalidChars)}]", string.Empty);
        }

        public static PathIdentity Identify(string path)
        {
            if (File.Exists(path))
                return PathIdentity.File;

            if (Directory.Exists(path))
                return PathIdentity.Directory;

            return PathIdentity.None;
        }
    }

    public enum PathIdentity
    {
        None,
        File,
        Directory
    }
}
