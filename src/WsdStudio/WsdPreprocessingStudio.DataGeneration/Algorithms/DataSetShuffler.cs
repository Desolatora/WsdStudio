using System.Collections.Generic;
using WsdPreprocessingStudio.Core.Extensions;
using WsdPreprocessingStudio.DataGeneration.Data;

namespace WsdPreprocessingStudio.DataGeneration.Algorithms
{
    public class DataSetShuffler
    {
        public void ShuffleData(IList<DataSetGroup> dataSetGroups)
        {
            foreach (var dataSetGroup in dataSetGroups)
            foreach (var dataSet in dataSetGroup.DataSets.Values)
            {
                dataSet.Data.Shuffle();
            }
        }
    }
}
