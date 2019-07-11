using System.Collections.Generic;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Plugins;
using WsdPreprocessingStudio.Core.Threading;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Plugins
{
    public interface IPluginDataGenerationHandler : IPluginComponent
    {
        int GetExecutionPriority(WsdProject project);

        void BeforeGenerationStarted(
            WsdProject project, GenerationInfo info, IProgressHandle progress);

        void AfterGenerationCompleted(
            WsdProject project, GenerationInfo info, IProgressHandle progress);

        void AfterDictionaryReordered(
            WordDictionary reorderedDictionary, WsdProject project, GenerationInfo info, 
            IProgressHandle progress);

        void AfterRecordsGenerated(
            Dictionary<DataSetName, DataSetByText> dataSets, WsdProject project, GenerationInfo info, 
            IProgressHandle progress);

        void AfterGroupsFormed(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info, 
            IProgressHandle progress);

        void AfterTestOnlySetExtracted(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info, 
            IProgressHandle progress);

        void AfterValidationSetExtracted(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info, 
            IProgressHandle progress);

        void AfterDataShuffled(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info, 
            IProgressHandle progress);

        void BeforeDataWritten(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info, 
            IProgressHandle progress);
    }
}
