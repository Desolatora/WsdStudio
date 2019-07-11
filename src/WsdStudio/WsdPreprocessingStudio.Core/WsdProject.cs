using System;
using System.IO;
using System.Linq;
using WsdPreprocessingStudio.Core.Data;
using WsdPreprocessingStudio.Core.Data.Collections;
using WsdPreprocessingStudio.Core.Data.Statistics;
using WsdPreprocessingStudio.Core.Helpers;
using WsdPreprocessingStudio.Core.IO.Readers.Input;
using WsdPreprocessingStudio.Core.IO.Readers.System;
using WsdPreprocessingStudio.Core.IO.Writers.Misc;
using WsdPreprocessingStudio.Core.IO.Writers.System;
using WsdPreprocessingStudio.Core.Resources;
using WsdPreprocessingStudio.Core.Threading;

namespace WsdPreprocessingStudio.Core
{
    public class WsdProject
    {
        private const string CurrentProjectVersion = "1";

        public WsdProjectInfo ProjectInfo { get; }
        public WordDictionary Dictionary { get; }
        public TextData[] TrainData { get; }
        public TextData[] TestData { get; }
        public EmbeddingDictionary WordEmbeddings { get; }
        public EmbeddingDictionary MeaningEmbeddings { get; }
        public WordAnalysisDictionary DataAnalysis { get; }
        public DictionaryStatistics DictionaryStatistics { get; }
        public DataStatistics DataStatistics { get; }
        public EmbeddingStatistics WordEmbeddingStatistics { get; }
        public EmbeddingStatistics MeaningEmbeddingStatistics { get; }
        public WsdPosList PosList { get; }
        public PluginData PluginData { get; }

        public bool HasMeaningEmbeddings => MeaningEmbeddings != null && MeaningEmbeddings.Any();

        private WsdProject(
            WsdProjectInfo projectInfo, WordDictionary dictionary,
            TextData[] trainData, TextData[] testData,
            EmbeddingDictionary wordEmbeddings, EmbeddingDictionary meaningEmbeddings,
            WordAnalysisDictionary dataAnalysis, DictionaryStatistics dictionaryStatistics,
            DataStatistics dataStatistics, EmbeddingStatistics wordEmbeddingStatistics,
            EmbeddingStatistics meaningEmbeddingStatistics)
        {
            ProjectInfo = projectInfo;
            Dictionary = dictionary;
            TrainData = trainData;
            TestData = testData;
            WordEmbeddings = wordEmbeddings;
            MeaningEmbeddings = meaningEmbeddings;
            DataAnalysis = dataAnalysis;
            DictionaryStatistics = dictionaryStatistics;
            DataStatistics = dataStatistics;
            WordEmbeddingStatistics = wordEmbeddingStatistics;
            MeaningEmbeddingStatistics = meaningEmbeddingStatistics;
            PosList = new WsdPosList(trainData);
            PluginData = new PluginData();
        }

        public static WsdProject CreateAndSave(
            WsdProjectCreateInfo info, string destinationPath, IProgressHandle progress)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            if (string.IsNullOrEmpty(destinationPath))
                throw new ArgumentNullException(nameof(destinationPath));

            if (PathEx.Identify(destinationPath) != PathIdentity.Directory ||
                Directory.GetFiles(destinationPath, "*", SearchOption.AllDirectories).Length > 0)
                throw new ArgumentException(ExceptionMessage.DestinationPathMustBeEmptyAndExisting);

            if (progress == null)
                throw new ArgumentNullException(nameof(progress));

            info.AssertIsValid();

            progress.SetMessageFormat(MessageFormat.LoadingDictionary_Bytes);

            var dictionary = InputDictionaryReader.ReadAll(info.DictionaryPath, progress);

            progress.SetMessageFormat(MessageFormat.ComputingDictionaryStatistics);

            var dictionaryStatistics = new DictionaryStatistics().Compute(dictionary, progress);

            TextData[] trainData;
            TextData[] testData;

            if (info.DataType == InputDataType.PlainText)
            {
                progress.SetMessageFormat(MessageFormat.LoadingTrainData_Files);

                trainData = InputPlainTextDataReader.ReadAllFiles(info.TrainDataPath, progress);

                progress.SetMessageFormat(MessageFormat.LoadingTestData_Files);

                testData = InputPlainTextDataReader.ReadAllFiles(info.TestDataPath, progress);
            }
            else
            {
                progress.SetMessageFormat(MessageFormat.LoadingSynsetMappings_Bytes);

                var synsetMappings = InputSynsetMappingReader.ReadAll(info.SynsetMappingsPath, progress);

                progress.SetMessageFormat(MessageFormat.LoadingTrainData_Files);

                trainData = InputXmlDataReader.Read(
                    info.TrainDataPath, info.TrainGoldKeyPath, synsetMappings, dictionary, 
                    out var trainXmlParseErrors, progress);

                if (trainXmlParseErrors != null && trainXmlParseErrors.Any())
                    XmlParseErrorWriter.WriteAll(
                        Path.Combine(destinationPath, FileName.TrainXmlParseErrors + FileExtension.Text),
                        trainXmlParseErrors);

                progress.SetMessageFormat(MessageFormat.LoadingTestData_Files);

                testData = InputXmlDataReader.Read(
                    info.TestDataPath, info.TestGoldKeyPath, synsetMappings, dictionary, 
                    out var testXmlParseErrors, progress);

                if (testXmlParseErrors != null && testXmlParseErrors.Any())
                    XmlParseErrorWriter.WriteAll(
                        Path.Combine(destinationPath, FileName.TestXmlParseErrors + FileExtension.Text),
                        testXmlParseErrors);
            }

            progress.SetMessageFormat(MessageFormat.AnalyzingData_Files);

            var dataAnalysis = new WordAnalysisDictionary()
                .Analyze(dictionary, trainData, testData, progress);

            progress.SetMessageFormat(MessageFormat.ComputingDataStatistics);

            var dataStatistics = new DataStatistics()
                .Compute(dictionary, dataAnalysis, progress);

            progress.SetMessageFormat(MessageFormat.LoadingWordEmbeddings_Bytes);

            var wordEmbeddings = InputEmbeddingReader.ReadAll(
                info.WordEmbeddingsPath, dataAnalysis.GetAllWordOccurrences(), progress);

            var wordEmbeddingStatistics = new EmbeddingStatistics().Compute(wordEmbeddings);

            EmbeddingDictionary meaningEmbeddings = null;

            var meaningEmbeddingStatistics = new EmbeddingStatistics();

            if (!string.IsNullOrWhiteSpace(info.MeaningEmbeddingsPath))
            {
                progress.SetMessageFormat(MessageFormat.LoadingMeaningEmbeddings_Bytes);

                meaningEmbeddings = InputEmbeddingReader.ReadAll(
                    info.MeaningEmbeddingsPath, dataAnalysis.GetAllMeaningOccurrences(), progress);

                meaningEmbeddingStatistics.Compute(meaningEmbeddings);
            }

            var projectInfo = new WsdProjectInfo
            {
                ProjectName = Path.GetFileName(destinationPath),
                ProjectVersion = CurrentProjectVersion,
                ApplicationVersion = typeof(WsdProject).Assembly.GetName().Version.ToString(),
                Dictionary = FileName.Dictionary + FileExtension.WsdData,
                TrainData = trainData.Select(x => new WsdProjectTextDataInfo
                {
                    Name = x.TextName,
                    Path = Path.Combine(FolderName.Train, x.TextName + FileExtension.WsdData)
                }).ToArray(),
                TestData = testData.Select(x => new WsdProjectTextDataInfo
                {
                    Name = x.TextName,
                    Path = Path.Combine(FolderName.Test, x.TextName + FileExtension.WsdData)
                }).ToArray(),
                WordEmbeddings = FileName.WordEmbeddings + FileExtension.WsdData,
                MeaningEmbeddings = meaningEmbeddings != null
                    ? FileName.MeaningEmbeddings + FileExtension.WsdData
                    : string.Empty,
                DataAnalysis = FileName.DataAnalysis + FileExtension.WsdData,
                DictionaryStatistics = FileName.DictionaryStatistics + FileExtension.WsdData,
                DataStatistics = FileName.DataStatistics + FileExtension.WsdData,
                WordEmbeddingsStatistics = FileName.WordEmbeddingsStatistics + FileExtension.WsdData,
                MeaningEmbeddingsStatistics = FileName.MeaningEmbeddingsStatistics + FileExtension.WsdData
            };

            progress.SetMessageFormat(MessageFormat.SavingDictionary_Words);

            SystemDictionaryWriter.WriteAll(
                Path.Combine(destinationPath, projectInfo.Dictionary), dictionary, progress);

            progress.SetMessageFormat(MessageFormat.SavingTrainData_Files);

            SystemDataWriter.WriteAllFiles(
                destinationPath,
                projectInfo.TrainData
                    .Select(x => (x.Path, trainData.Single(y => y.TextName == x.Name).Data))
                    .ToArray(),
                progress);

            progress.SetMessageFormat(MessageFormat.SavingTestData_Files);

            SystemDataWriter.WriteAllFiles(
                destinationPath,
                projectInfo.TestData
                    .Select(x => (x.Path, testData.Single(y => y.TextName == x.Name).Data))
                    .ToArray(),
                progress);

            progress.SetMessageFormat(MessageFormat.SavingWordEmbeddings_Embeddings);

            SystemEmbeddingWriter.WriteAll(
                Path.Combine(destinationPath, projectInfo.WordEmbeddings), wordEmbeddings, progress);

            if (meaningEmbeddings != null)
            {
                progress.SetMessageFormat(MessageFormat.SavingMeaningEmbeddings_Embeddings);

                SystemEmbeddingWriter.WriteAll(
                    Path.Combine(destinationPath, projectInfo.MeaningEmbeddings), meaningEmbeddings, progress);
            }

            progress.SetMessageFormat(MessageFormat.SavingDataAnalysis_Words);

            SystemDataAnalysisWriter.WriteAll(
                Path.Combine(destinationPath, projectInfo.DataAnalysis), dataAnalysis, progress);

            SystemJsonWriter.Write(
                Path.Combine(destinationPath, projectInfo.DictionaryStatistics), dictionaryStatistics);

            SystemJsonWriter.Write(
                Path.Combine(destinationPath, projectInfo.DataStatistics), dataStatistics);

            SystemJsonWriter.Write(
                Path.Combine(destinationPath, projectInfo.WordEmbeddingsStatistics), wordEmbeddingStatistics);

            SystemJsonWriter.Write(
                Path.Combine(destinationPath, projectInfo.MeaningEmbeddingsStatistics),
                meaningEmbeddingStatistics);

            SystemJsonWriter.Write(
                Path.Combine(destinationPath, projectInfo.ProjectName + FileExtension.WsdProj),
                projectInfo);

            return new WsdProject(
                projectInfo, dictionary, trainData, testData, wordEmbeddings, meaningEmbeddings,
                dataAnalysis, dictionaryStatistics, dataStatistics, wordEmbeddingStatistics,
                meaningEmbeddingStatistics);
        }

        public static WsdProject Load(string path, IProgressHandle progress)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            if (PathEx.Identify(path) != PathIdentity.File ||
                Path.GetExtension(path) != FileExtension.WsdProj)
                throw new ArgumentException(ExceptionMessage.PathMustBeExistingWsdProj);

            var projectInfo = SystemJsonReader.Read<WsdProjectInfo>(path);
            var projectDirectory = Path.GetDirectoryName(path);

            if (projectInfo.ProjectVersion != CurrentProjectVersion)
                throw new Exception(ExceptionMessage.ProjectVersionNotSupported(
                    projectInfo.ProjectVersion, CurrentProjectVersion));

            progress.SetMessageFormat(MessageFormat.LoadingDictionary_Bytes);

            var dictionary = SystemDictionaryReader.ReadAll(
                Path.Combine(projectDirectory, projectInfo.Dictionary), progress);

            progress.SetMessageFormat(MessageFormat.LoadingTrainData_Files);

            var trainData = SystemDataReader.ReadAllFiles(
                projectDirectory, projectInfo.TrainData.Select(x => (x.Path, x.Name)).ToArray(),
                progress);

            progress.SetMessageFormat(MessageFormat.LoadingTestData_Files);

            var testData = SystemDataReader.ReadAllFiles(
                projectDirectory, projectInfo.TestData.Select(x => (x.Path, x.Name)).ToArray(),
                progress);

            progress.SetMessageFormat(MessageFormat.LoadingWordEmbeddings_Bytes);

            var wordEmbeddings = SystemEmbeddingReader.ReadAll(
                Path.Combine(projectDirectory, projectInfo.WordEmbeddings), progress);

            EmbeddingDictionary meaningEmbeddings = null;

            if (!string.IsNullOrWhiteSpace(projectInfo.MeaningEmbeddings))
            {
                progress.SetMessageFormat(MessageFormat.LoadingMeaningEmbeddings_Bytes);

                meaningEmbeddings = SystemEmbeddingReader.ReadAll(
                    Path.Combine(projectDirectory, projectInfo.MeaningEmbeddings), progress);
            }

            progress.SetMessageFormat(MessageFormat.LoadingDataAnalysis_Bytes);

            var dataAnalysis = SystemDataAnalysisReader.ReadAll(
                Path.Combine(projectDirectory, projectInfo.DataAnalysis), progress);

            var dictionaryStatistics = SystemJsonReader.Read<DictionaryStatistics>(
                Path.Combine(projectDirectory, projectInfo.DictionaryStatistics));

            var dataStatistics = SystemJsonReader.Read<DataStatistics>(
                Path.Combine(projectDirectory, projectInfo.DataStatistics));

            var wordEmbeddingsStatistics = SystemJsonReader.Read<EmbeddingStatistics>(
                Path.Combine(projectDirectory, projectInfo.WordEmbeddingsStatistics));

            var meaningEmbeddingsStatistics = SystemJsonReader.Read<EmbeddingStatistics>(
                Path.Combine(projectDirectory, projectInfo.MeaningEmbeddingsStatistics));

            return new WsdProject(
                projectInfo, dictionary, trainData, testData, wordEmbeddings, meaningEmbeddings,
                dataAnalysis, dictionaryStatistics, dataStatistics, wordEmbeddingsStatistics,
                meaningEmbeddingsStatistics);
        }
    }
}