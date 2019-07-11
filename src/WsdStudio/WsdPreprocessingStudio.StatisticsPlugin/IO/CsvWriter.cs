using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace WsdPreprocessingStudio.StatisticsPlugin.IO
{
    public class CsvWriter : IDisposable
    {
        private CultureInfo _culture;
        private StreamWriter _writer;

        public CsvWriter(StreamWriter writer)
            : this(writer, CultureInfo.CurrentCulture)
        {
        }

        public CsvWriter(StreamWriter writer, CultureInfo culture)
        {
            _culture = culture ?? throw new ArgumentNullException(nameof(culture));
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void WriteLine(params object[] items)
        {
            if (items.Length == 0)
                return;

            var separator = _culture.TextInfo.ListSeparator;
            var values = items.Select(_quote).ToArray();

            _writer.WriteLine(string.Join(separator, values));
        }

        private string _quote(object item)
        {
            return "\"" + Convert.ToString(item).Replace("\"", "\"\"") + "\"";
        }

        public void Dispose()
        {
            _writer?.Dispose();
            _writer = null;
        }
    }
}
