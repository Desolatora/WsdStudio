namespace WsdPreprocessingStudio.Core.Data
{
    public class RawWordEncounter
    {
        public const string EmptyWord = "</0>";
        public const string EndOfSentence = "</s>";

        public static readonly RawWordEncounter EmptyWordEncounter = new RawWordEncounter
        {
            Word = EmptyWord
        };
        public static readonly RawWordEncounter EndOfSentenceEncounter = new RawWordEncounter
        {
            Word = EndOfSentence
        };

        private string _word;
        private string _pos;
        private string _meaning;

        public string Word
        {
            get => _word;
            set => _word = string.Intern(value);
        }

        public string Pos
        {
            get => _pos;
            set => _pos = string.Intern(value);
        }

        public string Meaning
        {
            get => _meaning;
            set => _meaning = string.Intern(value);
        }
    }
}