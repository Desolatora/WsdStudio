using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WsdPreprocessingStudio.Core.Helpers
{
    public static class StringEx
    {
        public const string ListSeparator = " ";
        public const string DefaultDoubleFormat = "R";

        public static string ToSize(long bytes)
        {
            string[] sizes = {"B", "KB", "MB", "GB", "TB"};

            var order = 0;
            var size = (double) bytes;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size = size / 1024;

                if (order >= sizes.Length - 1)
                    break;
            }
            
            return $"{size:0.##} {sizes[order]}";
        }

        public static string ToString(StringBuilder builder, string format, IEnumerable<float> enumerable)
        {
            if (string.IsNullOrWhiteSpace(format))
                format = DefaultDoubleFormat;

            var first = true;

            foreach (var element in enumerable)
            {
                if (!first)
                    builder.Append(ListSeparator);
                else
                    first = false;

                builder.Append(element.ToString(format, CultureInfo.InvariantCulture));
            }

            return builder.ToString();
        }

        public static string ToString(string format, IEnumerable<float> enumerable)
        {
            return ToString(new StringBuilder(), format, enumerable);
        }

        public static string ToString<TDoubleEnumerable>(
            StringBuilder builder, string format, IEnumerable<TDoubleEnumerable> enumerable)
            where TDoubleEnumerable : IEnumerable<float>
        {
            if (string.IsNullOrWhiteSpace(format))
                format = DefaultDoubleFormat;

            var first = true;

            foreach (var collection in enumerable)
            foreach (var element in collection)
            {
                if (!first)
                    builder.Append(ListSeparator);
                else
                    first = false;

                builder.Append(element.ToString(format, CultureInfo.InvariantCulture));
            }

            return builder.ToString();
        }

        public static string ToString(string format, IEnumerable<float[]> enumerable)
        {
            return ToString(new StringBuilder(), format, enumerable);
        }
    }
}