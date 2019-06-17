using System.Windows.Forms;
using WsdPreprocessingStudio.Core.Data.Statistics;
using WsdPreprocessingStudio.Core.UI;

namespace WsdPreprocessingStudio.Binders
{
    public class DataStatisticsBinder
    {
        public static void BindToListView(DataStatistics data, ListView control)
        {
            control
                .AddGroup("Data statistics (basic)", group =>
                {
                    group
                        .AddItem("Monosemantic train examples", data.MonosemanticTrainExamples.ToString())
                        .AddItem("Polysemantic train examples", data.PolysemanticTrainExamples.ToString())
                        .AddItem("Monosemantic test examples", data.MonosemanticTestExamples.ToString())
                        .AddItem("Polysemantic test examples", data.PolysemanticTestExamples.ToString())
                        .AddItem("Common test examples", data.CommonTestExamples.ToString())
                        .AddItem("Learnable test examples", data.LearnableTestExamples.ToString())
                        .AddItem("Nonlearnable test examples", data.NonLearnableTestExamples.ToString())
                        .AddItem("Test-only examples", data.TestOnlyExamples.ToString())
                        .AddItem(
                            "Correct dictionary-based learnable guesses",
                            data.CorrectDictionaryBasedLearnableGuesses.ToString())
                        .AddItem(
                            "Correct dictionary-based nonlearnable guesses",
                            data.CorrectDictionaryBasedNonLearnableGuesses.ToString())
                        .AddItem(
                            "Correct dictionary-based test-only guesses",
                            data.CorrectDictionaryBasedTestOnlyGuesses.ToString())
                        .AddItem(
                            "Correct POS dictionary-based learnable guesses",
                            data.CorrectPosDictionaryBasedLearnableGuesses.ToString())
                        .AddItem(
                            "Correct POS dictionary-based nonlearnable guesses",
                            data.CorrectPosDictionaryBasedNonLearnableGuesses.ToString())
                        .AddItem(
                            "Correct POS dictionary-based test-only guesses",
                            data.CorrectPosDictionaryBasedTestOnlyGuesses.ToString())
                        .AddItem(
                            "Correct training-based learnable guesses",
                            data.CorrectTrainingBasedLearnableGuesses.ToString())
                        .AddItem(
                            "Correct POS training-based learnable guesses",
                            data.CorrectPosTrainingBasedLearnableGuesses.ToString());
                })
                .AddGroup("Data statistics (baselines - common examples)", group =>
                {
                    group
                        .AddItem(
                            "First Sense Dictionary Baseline",
                            data.FirstSenseDictionaryBaseline.ToString("P"))
                        .AddItem(
                            "First Sense POS Dictionary Baseline",
                            data.FirstSensePosDictionaryBaseline.ToString("P"))
                        .AddItem(
                            "First Sense Baseline",
                            data.FirstSenseBaseline.ToString("P"))
                        .AddItem(
                            "First Sense POS Baseline",
                            data.FirstSensePosBaseline.ToString("P"));
                })
                .AddGroup("Data statistics (baselines - polysemantic examples)", group =>
                {
                    group
                        .AddItem(
                            "Best Case Baseline",
                            data.BestCaseBaseline.ToString("P"));
                })
                .AddGroup("Data statistics (baselines - all examples)", group =>
                {
                    group
                        .AddItem(
                            "First Sense Dictionary Baseline",
                            data.All_FirstSenseDictionaryBaseline.ToString("P"))
                        .AddItem(
                            "First Sense POS Dictionary Baseline",
                            data.All_FirstSensePosDictionaryBaseline.ToString("P"))
                        .AddItem(
                            "First Sense Baseline",
                            data.All_FirstSenseBaseline.ToString("P"))
                        .AddItem(
                            "First Sense POS Baseline",
                            data.All_FirstSensePosBaseline.ToString("P"))
                        .AddItem(
                            "Best Case Baseline",
                            data.All_BestCaseBaseline.ToString("P"));
                });
        }
    }
}
