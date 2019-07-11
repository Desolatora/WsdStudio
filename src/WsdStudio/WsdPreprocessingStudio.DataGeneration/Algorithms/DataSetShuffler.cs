using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Extensions;
using WsdPreprocessingStudio.Core.Threading;
using WsdPreprocessingStudio.DataGeneration.Data;
using WsdPreprocessingStudio.DataGeneration.Resources;

namespace WsdPreprocessingStudio.DataGeneration.Algorithms
{
    public class DataSetShuffler
    {
        public void ShuffleData(IList<DataSetGroup> dataSetGroups, IProgressHandle progress)
        {
            using (var scope = progress.Scope(dataSetGroups.Count, MessageFormat.ExtractingValidationSet_Groups))
            {
                var counter = 0;

                foreach (var dataSetGroup in dataSetGroups)
                {
                    scope.TrySet(counter++);

                    foreach (var dataSet in dataSetGroup.DataSets.Values)
                    {
                        dataSet.Data.Shuffle();
                    }
                }
            }
        }
    }
}
