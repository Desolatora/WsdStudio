using System;
using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Threading;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Resources;

namespace WsdPreprocessingStudio.DataGeneration.Algorithms
{
    public class DataSetGrouper
    {
        public IList<DataSetGroup> FormGroups(
            Dictionary<DataSetName, DataSetByText> dataSets, WsdProject project, GenerationInfo info,
            IProgressHandle progress)
        {
            var dataSetGroups = new Dictionary<string, DataSetGroup>();

            using (var scope = progress.Scope(dataSets.Count, MessageFormat.FormingGroups_DataSets))
            {
                var counter = 0;

                foreach (var dataSet in dataSets.Values)
                {
                    scope.TrySet(counter++);

                    IEnumerable<(string groupName, IEnumerable<RawRecord> data)> dataByGroup;

                    switch (info.SavingStrategy)
                    {
                        case SavingStrategy.SingleFile:
                        {
                            dataByGroup = dataSet.Texts
                                .SelectMany(x => x.Data)
                                .GroupBy(x => string.Empty)
                                .Select(x => (x.Key, (IEnumerable<RawRecord>) x));
                            break;
                        }
                        case SavingStrategy.FilePerWord:
                        {
                            dataByGroup = dataSet.Texts
                                .SelectMany(x => x.Data)
                                .GroupBy(x => x.Word + "__" + project.Dictionary.GetByName(x.Word).Id)
                                .Select(x => (x.Key, (IEnumerable<RawRecord>) x));
                            break;
                        }
                        case SavingStrategy.FilePerPos:
                        {
                            dataByGroup = dataSet.Texts
                                .SelectMany(x => x.Data)
                                .GroupBy(x => x.Pos)
                                .Select(x => (x.Key, (IEnumerable<RawRecord>) x));
                            break;
                        }
                        case SavingStrategy.FilePerWordAndPos:
                        {
                            dataByGroup = dataSet.Texts
                                .SelectMany(x => x.Data)
                                .GroupBy(x => x.Word + "__" + x.Pos + "__" + project.Dictionary.GetByName(x.Word).Id)
                                .Select(x => (x.Key, (IEnumerable<RawRecord>) x));
                            break;
                        }
                        case SavingStrategy.OriginalFiles:
                        {
                            dataByGroup = dataSet.Texts
                                .Select(x => (x.TextName, (IEnumerable<RawRecord>) x.Data));
                            break;
                        }
                        default:
                        {
                            throw new NotSupportedException(
                                $"Saving stragegy {info.SavingStrategy} is not supported.");
                        }
                    }

                    foreach (var (groupName, data) in dataByGroup)
                    {
                        if (!dataSetGroups.ContainsKey(groupName))
                            dataSetGroups[groupName] = new DataSetGroup(groupName);

                        dataSetGroups[groupName].DataSets[dataSet.Name] =
                            new DataSet(dataSet.Name, data.ToArray());
                    }
                }

                return dataSetGroups.Values.ToArray();
            }
        }
    }
}
