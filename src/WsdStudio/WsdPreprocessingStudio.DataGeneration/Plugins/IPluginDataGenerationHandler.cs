using System.Collections.Generic;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Plugins;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Plugins
{
    public interface IPluginDataGenerationHandler : IPluginComponent
    {
        void BeforeGenerationStarted(WsdProject project, GenerationInfo info);

        void AfterGenerationCompleted(WsdProject project, GenerationInfo info);

        void AfterDictionaryReordered(
            WordDictionary reorderedDictionary, WsdProject project, GenerationInfo info);

        void AfterRecordsGenerated(
            Dictionary<DataSetName, DataSetByText> dataSets, WsdProject project, GenerationInfo info);

        void AfterGroupsFormed(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info);

        void AfterTestOnlySetExtracted(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info);

        void AfterValidationSetExtracted(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info);

        void AfterDataShuffled(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info);

        void BeforeDataWritten(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info);
    }
}
