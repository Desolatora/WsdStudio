using System.Collections.Generic;
using System.Linq;
using WsdPreprocessingStudio.Core;
using WsdPreprocessingStudio.Core.Threading;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Resources;

namespace WsdPreprocessingStudio.DataGeneration.Algorithms
{
    public class TestOnlySetExtractor
    {
        public void Extract(
            IList<DataSetGroup> dataSetGroups, WsdProject project, GenerationInfo info,
            IProgressHandle progress)
        {
            using (var scope = progress.Scope(dataSetGroups.Count, MessageFormat.ExtractingTestOnlySet_Groups))
            {
                var counter = 0;

                foreach (var dataSetGroup in dataSetGroups)
                {
                    scope.TrySet(counter++);

                    var oldTestSet = dataSetGroup.DataSets.GetByName(DataSetName.Test);

                    if (oldTestSet == null)
                        continue;

                    var testExamples = oldTestSet.Data
                        .Where(x => project.DataAnalysis[x.Word].TrainEncounters.Any())
                        .ToArray();

                    var testOnlyExamples = oldTestSet.Data
                        .Where(x => !project.DataAnalysis[x.Word].TrainEncounters.Any())
                        .ToArray();

                    if (testExamples.Length > 0)
                    {
                        dataSetGroup.DataSets[DataSetName.Test] = new DataSet(DataSetName.Test, testExamples);
                    }
                    else
                    {
                        dataSetGroup.DataSets.Remove(DataSetName.Test);
                    }

                    if (testOnlyExamples.Length > 0)
                    {
                        dataSetGroup.DataSets[DataSetName.TestOnly] =
                            new DataSet(DataSetName.TestOnly, testOnlyExamples);
                    }
                    else
                    {
                        dataSetGroup.DataSets.Remove(DataSetName.TestOnly);
                    }
                }
            }
        }
    }
}
