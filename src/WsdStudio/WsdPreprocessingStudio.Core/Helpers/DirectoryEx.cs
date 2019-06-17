using System.IO;

namespace WsdPreprocessingStudio.Core.Helpers
{
    public static class DirectoryEx
    {
        public static void CreateEmptySafe(string directory)
        {
            Directory.CreateDirectory(directory);

            var directoryInfo = new DirectoryInfo(directory);

            foreach (var file in directoryInfo.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch
                {
                }
            }

            foreach (var subDirectory in directoryInfo.GetDirectories())
            {
                try
                {
                    subDirectory.Delete(true);
                }
                catch
                {
                }
            }
        }

        public static void CreateFor(string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);

            Directory.CreateDirectory(directory);
        }
    }
}