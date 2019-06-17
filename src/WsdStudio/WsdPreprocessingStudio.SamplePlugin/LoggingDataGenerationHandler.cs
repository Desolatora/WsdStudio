using System.Collections.Generic;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Plugins;

namespace WsdPreprocessingStudio.SamplePlugin
{
    public class LoggingDataGenerationHandler : IPluginDataGenerationHandler
    {
        public string DisplayName => "Logging data generation handler";

        public void AfterDataShuffled(IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info)
        {
            var logger = project.PluginData.GetData<LoggingPlugin, EventLogger>(string.Empty);
            
            logger.LogMessage("AfterDataShuffled() called.");
        }

        public void BeforeGenerationStarted(WsdProject project, GenerationInfo info)
        {
            var logger = project.PluginData.GetData<LoggingPlugin, EventLogger>(string.Empty);

            logger.LogMessage("Generation started.");
            logger.LogMessage("");
            logger.LogMessage("BeforeGenerationStarted() called.");
        }

        public void AfterGenerationCompleted(WsdProject project, GenerationInfo info)
        {
            var logger = project.PluginData.GetData<LoggingPlugin, EventLogger>(string.Empty);
            var statistics = project.PluginData.GetData<LoggingPlugin, UsageStatistics>(string.Empty);

            logger.LogMessage("AfterGenerationCompleted() called.");
            logger.LogMessage("");
            logger.LogMessage("Generation completed.");
            logger.LogMessage("");
            logger.LogMessage("Usage statistics:");
            logger.LogMessage($"    Colocation source - {statistics.ColocationSourceCounter}");
            logger.LogMessage($"    CosThetaUnitary function - {statistics.CosThetaUnitaryCounter}");
            logger.LogMessage($"    String concat - {statistics.StringConcatCounter}");
            logger.LogMessage($"    Word element - {statistics.WordElementCounter}");
        }

        public void AfterDictionaryReordered(WordDictionary reorderedDictionary, WsdProject project, GenerationInfo info)
        {
            var logger = project.PluginData.GetData<LoggingPlugin, EventLogger>(string.Empty);

            logger.LogMessage("AfterDictionaryReordered() called.");
        }

        public void AfterGroupsFormed(IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info)
        {
            var logger = project.PluginData.GetData<LoggingPlugin, EventLogger>(string.Empty);

            logger.LogMessage("AfterGroupsFormed() called.");
        }

        public void AfterRecordsGenerated(Dictionary<DataSetName, DataSetByText> dataSets, WsdProject project, GenerationInfo info)
        {
            var logger = project.PluginData.GetData<LoggingPlugin, EventLogger>(string.Empty);

            logger.LogMessage("AfterRecordsGenerated() called.");
        }

        public void AfterTestOnlySetExtracted(IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info)
        {
            var logger = project.PluginData.GetData<LoggingPlugin, EventLogger>(string.Empty);

            logger.LogMessage("AfterTestOnlySetExtracted() called.");
        }

        public void AfterValidationSetExtracted(IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info)
        {
            var logger = project.PluginData.GetData<LoggingPlugin, EventLogger>(string.Empty);

            logger.LogMessage("AfterValidationSetExtracted() called.");
        }

        public void BeforeDataWritten(IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info)
        {
            var logger = project.PluginData.GetData<LoggingPlugin, EventLogger>(string.Empty);

            logger.LogMessage("BeforeDataWritten() called.");
        }
    }
}
