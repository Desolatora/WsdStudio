using WsdPreprocessingStudio.Core.Data.Collections;

namespace WsdPreprocessingStudio.Core.Data
{
    public class WordAnalysis
    {
        private string _word;

        public int Id { get; set; }

        public string Word
        {
            get => _word;
            set => _word = string.Intern(value);
        }

        public MeaningDictionary TrainEncounters { get; set; }
        public MeaningDictionary TestEncounters { get; set; }
        public MeaningDictionary AllEncounters { get; set; }

        public WordAnalysis()
        {
            TrainEncounters = new MeaningDictionary();
            TestEncounters = new MeaningDictionary();
            AllEncounters = new MeaningDictionary();
        }
    }
}
