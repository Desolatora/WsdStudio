using System;

namespace WsdPreprocessingStudio.Core.Data
{
    public class TextData
    {
        public string TextName { get; }
        public RawWordEncounter[] Data { get; }

        public TextData(string textName, RawWordEncounter[] data)
        {
            TextName = textName ?? throw new ArgumentNullException(nameof(textName));
            Data = data ?? new RawWordEncounter[0];
        }
    }
}
