using System;
using System.Collections.Generic;
using System.IO;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Threading;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Plugins;
using WsdPreprocessingStudio.StatisticsPlugin.Data;
using WsdPreprocessingStudio.StatisticsPlugin.IO;
using WsdPreprocessingStudio.StatisticsPlugin.Resources;

namespace WsdPreprocessingStudio.StatisticsPlugin
{
    public class StatisticsHandler : IPluginDataGenerationHandler
    {
        public string DisplayName => "Data generation statistics handler";
        
        public int GetExecutionPriority(WsdProject project)
        {
            var config = project.PluginData.GetData<StatisticsPlugin, StatisticsConfig>(string.Empty);

            return config.HandlerExecutionPriority;
        }

        public void AfterDataShuffled(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info, 
            IProgressHandle progress)
        {
        }

        public void AfterDictionaryReordered(
            WordDictionary reorderedDictionary, WsdProject project, GenerationInfo info, 
            IProgressHandle progress)
        {
            project.PluginData.SetData<StatisticsPlugin, WordDictionary>(string.Empty, reorderedDictionary);
        }

        public void AfterGenerationCompleted(
            WsdProject project, GenerationInfo info, IProgressHandle progress)
        {
        }

        public void AfterGroupsFormed(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info, 
            IProgressHandle progress)
        {
        }

        public void AfterRecordsGenerated(
            Dictionary<DataSetName, DataSetByText> dataSets, WsdProject project, GenerationInfo info, 
            IProgressHandle progress)
        {
        }

        public void AfterTestOnlySetExtracted(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info, 
            IProgressHandle progress)
        {
        }

        public void AfterValidationSetExtracted(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info, 
            IProgressHandle progress)
        {
        }

        public void BeforeDataWritten(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info, 
            IProgressHandle progress)
        {
            var config = project.PluginData.GetData<StatisticsPlugin, StatisticsConfig>(string.Empty);

            if (!config.PluginEnabled)
                return;

            var dictionary = project.PluginData.GetData<StatisticsPlugin, WordDictionary>(string.Empty);
            var filePath = Path.Combine(
                info.DestinationFolder, FileName.DataSetStatistics + FileExtension.Csv);

            using (var streamWriter = new StreamWriter(filePath))
            using (var writer = new CsvWriter(streamWriter))
            using (var scope = progress.Scope(dataSetGroups.Count, MessageFormat.ComputingStatistics_Groups))
            {
                writer.WriteLine(
                    "Group", "Train examples", "Validation examples", "Test examples", "Test-only examples",
                    "Majority vote", "Train classes", "Test classes", "Train entropy", "Test entropy");
                
                for (var i = 0; i < dataSetGroups.Count; i++)
                {
                    scope.TrySet(i);

                    var dataSetGroup = dataSetGroups[i];
                    var statistics = DataSetGroupStatistics.Compute(dictionary, dataSetGroup);

                    if (config.RequireTrainingSet && statistics.TrainExamples == 0 ||
                        config.RequireTestSet && statistics.TestExamples == 0 ||
                        statistics.TrainExamples + statistics.ValidationExamples <
                        config.MinimumTrainingValidationExamples)
                        continue;

                    writer.WriteLine(
                        dataSetGroup.GroupName, statistics.TrainExamples, statistics.ValidationExamples,
                        statistics.TestExamples, statistics.TestOnlyExamples, statistics.MajorityVote,
                        statistics.TrainClasses, statistics.TestClasses, statistics.TrainEntropy,
                        statistics.TestEntropy);
                }
            }

            if (config.AbortGenerationAfterStatisticsAreComputed)
                throw new OperationCanceledException();
        }

        public void BeforeGenerationStarted(
            WsdProject project, GenerationInfo info, IProgressHandle progress)
        {
        }
    }
}
