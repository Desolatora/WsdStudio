using System.Linq;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core.Data.Statistics
{
    public class DataStatistics
    {
        public int MonosemanticTrainExamples { get; set; }
        public int PolysemanticTrainExamples { get; set; }
        public int MonosemanticTestExamples { get; set; }
        public int PolysemanticTestExamples { get; set; }
        public int CommonTestExamples { get; set; }
        public int LearnableTestExamples { get; set; }
        public int NonLearnableTestExamples { get; set; }
        public int TestOnlyExamples { get; set; }
        public int CorrectDictionaryBasedLearnableGuesses { get; set; }
        public int CorrectDictionaryBasedNonLearnableGuesses { get; set; }
        public int CorrectDictionaryBasedTestOnlyGuesses { get; set; }
        public int CorrectPosDictionaryBasedLearnableGuesses { get; set; }
        public int CorrectPosDictionaryBasedNonLearnableGuesses { get; set; }
        public int CorrectPosDictionaryBasedTestOnlyGuesses { get; set; }
        public int CorrectTrainingBasedLearnableGuesses { get; set; }
        public int CorrectPosTrainingBasedLearnableGuesses { get; set; }

        public double FirstSenseDictionaryBaseline { get; set; }
        public double FirstSensePosDictionaryBaseline { get; set; }
        public double FirstSenseBaseline { get; set; }
        public double FirstSensePosBaseline { get; set; }
        public double BestCaseBaseline { get; set; }

        public double All_FirstSenseDictionaryBaseline { get; set; }
        public double All_FirstSensePosDictionaryBaseline { get; set; }
        public double All_FirstSenseBaseline { get; set; }
        public double All_FirstSensePosBaseline { get; set; }
        public double All_BestCaseBaseline { get; set; }

        public DataStatistics Compute(
            WordDictionary dictionary, WordAnalysisDictionary dataAnalysis, IProgressHandle progress = null)
        {
            var scope = progress?.Scope(1);

            try
            {
                MonosemanticTrainExamples = dataAnalysis.Values
                    .Where(x => (dictionary.GetByName(x.Word)?.Meanings.Count ?? 0) == 1)
                    .Sum(x => x.TrainEncounters.Values.Sum(y => y.Encounters));

                PolysemanticTrainExamples = dataAnalysis.Values
                    .Where(x => (dictionary.GetByName(x.Word)?.Meanings.Count ?? 0) > 1)
                    .Sum(x => x.TrainEncounters.Values.Sum(y => y.Encounters));

                MonosemanticTestExamples = dataAnalysis.Values
                    .Where(x => (dictionary.GetByName(x.Word)?.Meanings.Count ?? 0) == 1)
                    .Sum(x => x.TestEncounters.Values.Sum(y => y.Encounters));

                PolysemanticTestExamples = dataAnalysis.Values
                    .Where(x => (dictionary.GetByName(x.Word)?.Meanings.Count ?? 0) > 1)
                    .Sum(x => x.TestEncounters.Values.Sum(y => y.Encounters));

                var commonAnalysis = dataAnalysis.Values
                    .Where(x => (dictionary.GetByName(x.Word)?.Meanings.Count ?? 0) > 1)
                    .Where(x => x.TrainEncounters.Count != 0)
                    .SelectMany(x => x.TestEncounters
                        .Select(y => new
                        {
                            x.Word,
                            y.Value.Meaning,
                            y.Value.PartOfSpeech,
                            y.Value.Encounters
                        }))
                    .ToArray();

                var learnableAnalysis = dataAnalysis.Values
                    .Where(x => (dictionary.GetByName(x.Word)?.Meanings.Count ?? 0) > 1)
                    .Where(x => x.TrainEncounters.Count != 0)
                    .SelectMany(x => x.TestEncounters
                        .Where(y => x.TrainEncounters.ContainsKey(y.Key))
                        .Select(y => new
                        {
                            x.Word,
                            y.Value.Meaning,
                            y.Value.PartOfSpeech,
                            y.Value.Encounters
                        }))
                    .ToArray();

                var nonLearnableAnalysis = dataAnalysis.Values
                    .Where(x => (dictionary.GetByName(x.Word)?.Meanings.Count ?? 0) > 1)
                    .Where(x => x.TrainEncounters.Count != 0)
                    .SelectMany(x => x.TestEncounters
                        .Where(y => !x.TrainEncounters.ContainsKey(y.Key))
                        .Select(y => new
                        {
                            x.Word,
                            y.Value.Meaning,
                            y.Value.PartOfSpeech,
                            y.Value.Encounters
                        }))
                    .ToArray();

                var testOnlyAnalysis = dataAnalysis.Values
                    .Where(x => (dictionary.GetByName(x.Word)?.Meanings.Count ?? 0) > 1)
                    .Where(x => x.TrainEncounters.Count == 0)
                    .SelectMany(x => x.TestEncounters
                        .Select(y => new
                        {
                            x.Word,
                            y.Value.Meaning,
                            y.Value.PartOfSpeech,
                            y.Value.Encounters
                        }))
                    .ToArray();

                CommonTestExamples = commonAnalysis.Sum(x => x.Encounters);
                LearnableTestExamples = learnableAnalysis.Sum(x => x.Encounters);
                NonLearnableTestExamples = nonLearnableAnalysis.Sum(x => x.Encounters);
                TestOnlyExamples = testOnlyAnalysis.Sum(x => x.Encounters);

                CorrectDictionaryBasedLearnableGuesses = learnableAnalysis
                    .Where(x => dictionary
                                    .GetByName(x.Word)
                                    .Meanings.Values
                                    .OrderByDescending(y => y.Encounters)
                                    .First()
                                    .Meaning ==
                                x.Meaning)
                    .Sum(x => x.Encounters);

                CorrectDictionaryBasedNonLearnableGuesses = nonLearnableAnalysis
                    .Where(x => dictionary
                                    .GetByName(x.Word)
                                    .Meanings.Values
                                    .OrderByDescending(y => y.Encounters)
                                    .First()
                                    .Meaning ==
                                x.Meaning)
                    .Sum(x => x.Encounters);

                CorrectDictionaryBasedTestOnlyGuesses = testOnlyAnalysis
                    .Where(x => dictionary
                                    .GetByName(x.Word)
                                    .Meanings.Values
                                    .OrderByDescending(y => y.Encounters)
                                    .First()
                                    .Meaning ==
                                x.Meaning)
                    .Sum(x => x.Encounters);

                CorrectPosDictionaryBasedLearnableGuesses = learnableAnalysis
                    .Where(x => dictionary
                                    .GetByName(x.Word)
                                    .Meanings.Values
                                    .OrderByDescending(y => y.Encounters)
                                    .First(y => y.PartOfSpeech == x.PartOfSpeech)
                                    .Meaning ==
                                x.Meaning)
                    .Sum(x => x.Encounters);

                CorrectPosDictionaryBasedNonLearnableGuesses = nonLearnableAnalysis
                    .Where(x => dictionary
                                    .GetByName(x.Word)
                                    .Meanings.Values
                                    .OrderByDescending(y => y.Encounters)
                                    .First(y => y.PartOfSpeech == x.PartOfSpeech)
                                    .Meaning ==
                                x.Meaning)
                    .Sum(x => x.Encounters);

                CorrectPosDictionaryBasedTestOnlyGuesses = testOnlyAnalysis
                    .Where(x => dictionary
                                    .GetByName(x.Word)
                                    .Meanings.Values
                                    .OrderByDescending(y => y.Encounters)
                                    .First(y => y.PartOfSpeech == x.PartOfSpeech)
                                    .Meaning ==
                                x.Meaning)
                    .Sum(x => x.Encounters);

                CorrectTrainingBasedLearnableGuesses = learnableAnalysis
                    .Where(x => dataAnalysis[x.Word]
                                    .TrainEncounters
                                    .Values
                                    .OrderByDescending(y => y.Encounters)
                                    .FirstOrDefault()
                                    .Meaning ==
                                x.Meaning)
                    .Sum(x => x.Encounters);

                CorrectPosTrainingBasedLearnableGuesses = learnableAnalysis
                    .Where(x => dataAnalysis[x.Word]
                                    .TrainEncounters
                                    .Values
                                    .Where(y => y.PartOfSpeech == x.PartOfSpeech)
                                    .OrderByDescending(y => y.Encounters)
                                    .FirstOrDefault()
                                    .Meaning ==
                                x.Meaning)
                    .Sum(x => x.Encounters);

                FirstSenseDictionaryBaseline = (CorrectDictionaryBasedLearnableGuesses +
                                                CorrectDictionaryBasedNonLearnableGuesses) /
                                               (double) CommonTestExamples;

                All_FirstSenseDictionaryBaseline = (CorrectDictionaryBasedLearnableGuesses +
                                                    CorrectDictionaryBasedNonLearnableGuesses +
                                                    CorrectDictionaryBasedTestOnlyGuesses +
                                                    MonosemanticTestExamples) /
                                                   (double) (PolysemanticTestExamples + MonosemanticTestExamples);

                FirstSensePosDictionaryBaseline = (CorrectPosDictionaryBasedLearnableGuesses +
                                                   CorrectPosDictionaryBasedNonLearnableGuesses) /
                                                  (double) CommonTestExamples;

                All_FirstSensePosDictionaryBaseline = (CorrectPosDictionaryBasedLearnableGuesses +
                                                       CorrectPosDictionaryBasedNonLearnableGuesses +
                                                       CorrectPosDictionaryBasedTestOnlyGuesses +
                                                       MonosemanticTestExamples) /
                                                      (double) (PolysemanticTestExamples +
                                                                MonosemanticTestExamples);

                FirstSenseBaseline = CorrectTrainingBasedLearnableGuesses /
                                     (double) CommonTestExamples;

                All_FirstSenseBaseline = (CorrectTrainingBasedLearnableGuesses + MonosemanticTestExamples) /
                                         (double) (PolysemanticTestExamples + MonosemanticTestExamples);

                FirstSensePosBaseline = CorrectPosTrainingBasedLearnableGuesses /
                                        (double) CommonTestExamples;

                All_FirstSensePosBaseline = (CorrectPosTrainingBasedLearnableGuesses + MonosemanticTestExamples) /
                                            (double) (PolysemanticTestExamples + MonosemanticTestExamples);

                BestCaseBaseline = (CorrectDictionaryBasedTestOnlyGuesses +
                                    LearnableTestExamples) /
                                   (double) PolysemanticTestExamples;

                All_BestCaseBaseline = (CorrectDictionaryBasedTestOnlyGuesses +
                                        LearnableTestExamples + MonosemanticTestExamples) /
                                       (double) (PolysemanticTestExamples + MonosemanticTestExamples);
            }
            finally
            {
                scope?.Dispose();
            }

            return this;
        }
    }
}
