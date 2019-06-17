using System;
using WsdPreprocessingStudio.Core.Data.Collections;

namespace WsdPreprocessingStudio.Core.Data
{
    public class DictionaryWord : IComparable<DictionaryWord>
    {
        private string _word;

        public int Id { get; set; }

        public string Word
        {
            get => _word;
            set => _word = string.Intern(value);
        }

        public MeaningDictionary Meanings { get; set; }

        public int CompareTo(DictionaryWord other)
        {
            return string.Compare(Word, other.Word, StringComparison.Ordinal);
        }
    }

    public class DictionaryMeaning
    {
        private string _meaning;
        private string _partOfSpeech;

        public int Id { get; set; }

        public string Meaning
        {
            get => _meaning;
            set => _meaning = string.Intern(value);
        }

        public string PartOfSpeech
        {
            get => _partOfSpeech;
            set => _partOfSpeech = string.Intern(value);
        }

        public int Encounters { get; set; }
    }
}