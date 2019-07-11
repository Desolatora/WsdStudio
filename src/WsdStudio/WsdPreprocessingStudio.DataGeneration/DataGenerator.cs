using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Helpers;
using WsdPreprocessingStudio.Core.IO.Writers.System;
using WsdPreprocessingStudio.Core.Plugins;
using WsdPreprocessingStudio.Core.Threading;
using WsdPreprocessingStudio.DataGeneration.Algorithms;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Features;
using WsdPreprocessingStudio.DataGeneration.Plugins;
using WsdPreprocessingStudio.DataGeneration.Resources;

namespace WsdPreprocessingStudio.DataGeneration
{
    public class DataGenerator
    {
        private ClassDeterminator _classDeterminator;
        private DataSetGrouper _dataSetGrouper;
        private DataSetShuffler _dataSetShuffler;
        private DataSetWriter _dataSetWriter;
        private GenerationAlgorithm _generationAlgorithm;
        private TestOnlySetExtractor _testOnlySetExtractor;
        private ValidationSetExtractor _validationSetExtractor;
        private IPluginDataGenerationHandler[] _dataGenerationHandlers;

        public DataGenerator(IPluginComponent[] pluginComponents)
        {
            _classDeterminator = new ClassDeterminator();
            _dataSetGrouper = new DataSetGrouper();
            _dataSetShuffler = new DataSetShuffler();
            _dataSetWriter = new DataSetWriter();
            _generationAlgorithm = new GenerationAlgorithm();
            _testOnlySetExtractor = new TestOnlySetExtractor();
            _validationSetExtractor = new ValidationSetExtractor();
            _dataGenerationHandlers = pluginComponents
                .Where(x => x is IPluginDataGenerationHandler)
                .Cast<IPluginDataGenerationHandler>()
                .ToArray();
        }

        public void Generate(
            WsdProject project, GenerationInfo info, IProgressHandle progress)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            if (PathEx.Identify(info.DestinationFolder) != PathIdentity.Directory ||
                Directory.GetFiles(info.DestinationFolder, "*", SearchOption.AllDirectories).Length > 0)
                throw new ArgumentException("DestinationFolder must be an empty existing directory.");

            info.AssertIsValid();

            var handlers = _dataGenerationHandlers
                .OrderBy(x => x.GetExecutionPriority(project))
                .ToArray();

            foreach (var handler in handlers)
            {
                handler.BeforeGenerationStarted(project, info, progress);
            }

            var reorderedDictionary = _classDeterminator.GetReorderedDictionary(project, info, progress);

            foreach (var handler in handlers)
            {
                handler.AfterDictionaryReordered(reorderedDictionary, project, info, progress);
            }

            var dataSets = new Dictionary<DataSetName, DataSetByText>
            {
                [DataSetName.Train] = new DataSetByText(
                    DataSetName.Train,
                    _generationAlgorithm.GenerateRecords(project.TrainData, project, info, progress)),

                [DataSetName.Test] = new DataSetByText(
                    DataSetName.Test,
                    _generationAlgorithm.GenerateRecords(project.TestData, project, info, progress))
            };

            foreach (var handler in handlers)
            {
                handler.AfterRecordsGenerated(dataSets, project, info, progress);
            }

            var dataSetGroups = _dataSetGrouper.FormGroups(dataSets, project, info, progress);

            foreach (var handler in handlers)
            {
                handler.AfterGroupsFormed(dataSetGroups, project, info, progress);
            }

            _testOnlySetExtractor.Extract(dataSetGroups, project, info, progress);

            foreach (var handler in handlers)
            {
                handler.AfterTestOnlySetExtracted(dataSetGroups, project, info, progress);
            }

            if (info.ExtractValidationSet)
            {
                _validationSetExtractor.Extract(dataSetGroups, info, progress);

                foreach (var handler in handlers)
                {
                    handler.AfterValidationSetExtracted(dataSetGroups, project, info, progress);
                }
            }

            if (info.ShuffleData)
            {
                _dataSetShuffler.ShuffleData(dataSetGroups, progress);

                foreach (var handler in handlers)
                {
                    handler.AfterDataShuffled(dataSetGroups, project, info, progress);
                }
            }

            var context = new FeatureSelectionContext
            {
                GenerationInfo = info,
                ReorderedDictionary = reorderedDictionary,
                FilteredPosList = new WsdPosList(info.FilteredPosList),
                Project = project
            };

            foreach (var handler in handlers)
            {
                handler.BeforeDataWritten(dataSetGroups, project, info, progress);
            }

            _dataSetWriter.WriteData(info.DestinationFolder, dataSetGroups, context, progress);

            SystemJsonWriter.Write(
                Path.Combine(
                    info.DestinationFolder,
                    FileName.GenerationInfo + FileExtension.WsdGenInfo),
                info);

            SystemJsonWriter.Write(
                Path.Combine(
                    info.DestinationFolder,
                    FileName.GenerationInfo + FileExtension.Text),
                new GenerationInfoReadable(info),
                null, false);

            foreach (var handler in handlers)
            {
                handler.AfterGenerationCompleted(project, info, progress);
            }
        }
    }
}
