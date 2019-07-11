using System;
using System.Linq;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.StatisticsPlugin.Data
{
    public class DataSetGroupStatistics
    {
        public int TrainExamples { get; set; }
        public int ValidationExamples { get; set; }
        public int TestExamples { get; set; }
        public int TestOnlyExamples { get; set; }
        public double MajorityVote { get; set; }
        public int TrainClasses { get; set; }
        public int TestClasses { get; set; }
        public double TrainEntropy { get; set; }
        public double TestEntropy { get; set; }

        public static DataSetGroupStatistics Compute(WordDictionary dictionary, DataSetGroup group)
        {
            var statistics = new DataSetGroupStatistics();
            var trainSet = group.DataSets.GetByName(DataSetName.Train);
            var validationSet = group.DataSets.GetByName(DataSetName.Validation);
            var testSet = group.DataSets.GetByName(DataSetName.Test);
            var testOnlySet = group.DataSets.GetByName(DataSetName.TestOnly);

            if (trainSet != null && trainSet.Data.Any())
            {
                var trainStats = trainSet.Data
                    .GroupBy(x => dictionary.GetByName(x.Word)?.Meanings.GetByName(x.Meaning)?.Id ?? 0)
                    .Select(x => new ClassStatistics
                    {
                        Class = x.Key,
                        Encounters = x.Count()
                    })
                    .ToArray();

                statistics.TrainExamples = trainSet.Data.Count;
                statistics.TrainClasses = trainStats.Length;
                statistics.TrainEntropy = -trainStats
                    .Sum(x => x.Encounters / (double) statistics.TrainExamples *
                              Math.Log(x.Encounters / (double) statistics.TrainExamples, 2));
            }

            if (testSet != null && testSet.Data.Any())
            {
                var testStats = testSet.Data
                    .GroupBy(x => dictionary.GetByName(x.Word)?.Meanings.GetByName(x.Meaning)?.Id ?? 0)
                    .Select(x => new ClassStatistics
                    {
                        Class = x.Key,
                        Encounters = x.Count()
                    })
                    .ToArray();

                statistics.TestExamples = testSet.Data.Count;
                statistics.TestClasses = testStats.Length;
                statistics.TestEntropy = -testStats
                    .Sum(x => x.Encounters / (double) statistics.TestExamples *
                              Math.Log(x.Encounters / (double) statistics.TestExamples, 2));
                statistics.MajorityVote = (testStats.SingleOrDefault(x => x.Class == 1)?.Encounters ?? 0) /
                                          (double) statistics.TestExamples;
            }

            if (validationSet != null && validationSet.Data.Any())
            {
                statistics.ValidationExamples = validationSet.Data.Count;
            }

            if (testOnlySet != null && testOnlySet.Data.Any())
            {
                statistics.TestOnlyExamples = testOnlySet.Data.Count;
            }

            return statistics;
        }
    }
}
