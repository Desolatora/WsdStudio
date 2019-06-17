using System.Collections.Generic;
using System.IO;
using System.Linq;
using WsdPreprocessingStudio.Core.Helpers;
using WsdPreprocessingStudio.Core.Threading;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Features;
using WsdPreprocessingStudio.DataGeneration.IO.Writers;
using WsdPreprocessingStudio.DataGeneration.Resources;

namespace WsdPreprocessingStudio.DataGeneration.Algorithms
{
    public class DataSetWriter
    {
        public void WriteData(
            string path, IList<DataSetGroup> dataSetGroups,
            FeatureSelectionContext context, IProgressHandle progress)
        {
            if (dataSetGroups.Count == 1)
            {
                foreach (var dataSet in dataSetGroups[0].DataSets.Values)
                {
                    if (dataSet.Data.Count == 0)
                        continue;

                    var fileName = Path.Combine(path, dataSet.Name.ToString());

                    context.DataSetName = dataSet.Name;

                    using (var writer = new OutputDataWriter(fileName, context))
                    {
                        progress.SetMessageFormat((c, m) =>
                            MessageFormat.SavingDataSet_Records(c, m, dataSet.Name.ToString()));

                        writer.WriteAll(dataSet.Data, progress);
                    }
                }
            }
            else
            {
                var dataSetCount = dataSetGroups.Sum(x => x.DataSets.Count);

                using (var scope = progress.Scope(dataSetCount, MessageFormat.SavingDataSets_Files))
                {
                    var counter = 0;

                    foreach (var dataSetGroup in dataSetGroups)
                    {
                        foreach (var dataSet in dataSetGroup.DataSets.Values)
                        {
                            if (dataSet.Data.Count == 0)
                            {
                                scope.TrySet(++counter);

                                continue;
                            }

                            var fileName = Path.Combine(
                                path, dataSet.Name.ToString(), PathEx.CleanFileName(dataSetGroup.GroupName));

                            context.DataSetName = dataSet.Name;

                            using (var writer = new OutputDataWriter(fileName, context))
                            {
                                writer.WriteAll(dataSet.Data);
                            }

                            scope.TrySet(++counter);
                        }
                    }
                }
            }
        }
    }
}
