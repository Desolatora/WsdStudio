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

            IList<DataSetGroup> dataSetGroups;
            FeatureSelectionContext context;

            foreach (var handler in _dataGenerationHandlers)
            {
                handler.BeforeGenerationStarted(project, info);
            }

            using (progress.Scope(1, MessageFormat.GeneratingData))
            {
                var reorderedDictionary = _classDeterminator.GetReorderedDictionary(project, info);

                foreach (var handler in _dataGenerationHandlers)
                {
                    handler.AfterDictionaryReordered(reorderedDictionary, project, info);
                }

                var dataSets = new Dictionary<DataSetName, DataSetByText>
                {
                    [DataSetName.Train] = new DataSetByText(
                        DataSetName.Train,
                        _generationAlgorithm.GenerateRecords(project.TrainData, project, info)),

                    [DataSetName.Test] = new DataSetByText(
                        DataSetName.Test,
                        _generationAlgorithm.GenerateRecords(project.TestData, project, info))
                };

                foreach (var handler in _dataGenerationHandlers)
                {
                    handler.AfterRecordsGenerated(dataSets, project, info);
                }

                dataSetGroups = _dataSetGrouper.FormGroups(dataSets, project, info);

                foreach (var handler in _dataGenerationHandlers)
                {
                    handler.AfterGroupsFormed(dataSetGroups, project, info);
                }

                _testOnlySetExtractor.Extract(dataSetGroups, project, info);

                foreach (var handler in _dataGenerationHandlers)
                {
                    handler.AfterTestOnlySetExtracted(dataSetGroups, project, info);
                }

                if (info.ExtractValidationSet)
                {
                    _validationSetExtractor.Extract(dataSetGroups, info);

                    foreach (var handler in _dataGenerationHandlers)
                    {
                        handler.AfterValidationSetExtracted(dataSetGroups, project, info);
                    }
                }

                if (info.ShuffleData)
                {
                    _dataSetShuffler.ShuffleData(dataSetGroups);

                    foreach (var handler in _dataGenerationHandlers)
                    {
                        handler.AfterDataShuffled(dataSetGroups, project, info);
                    }
                }

                context = new FeatureSelectionContext
                {
                    GenerationInfo = info,
                    ReorderedDictionary = reorderedDictionary,
                    FilteredPosList = new WsdPosList(info.FilteredPosList),
                    Project = project
                };
            }

            foreach (var handler in _dataGenerationHandlers)
            {
                handler.BeforeDataWritten(dataSetGroups, project, info);
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

            foreach (var handler in _dataGenerationHandlers)
            {
                handler.AfterGenerationCompleted(project, info);
            }
        }
    }
}
